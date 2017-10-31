﻿using System;
using System.Collections.Generic;
using System.Globalization;
using Umbraco.Core;
using Umbraco.Core.Models.PublishedContent;
using Umbraco.Core.PropertyEditors;
using Umbraco.Web.PublishedCache;

namespace Umbraco.Web.PropertyEditors.ValueConverters
{
    internal class ContentPickerValueConverter : PropertyValueConverterBase
    {
        private readonly IPublishedSnapshotAccessor _publishedSnapshotAccessor;

        private static readonly List<string> PropertiesToExclude = new List<string>
        {
            Constants.Conventions.Content.InternalRedirectId.ToLower(CultureInfo.InvariantCulture),
            Constants.Conventions.Content.Redirect.ToLower(CultureInfo.InvariantCulture)
        };

        public ContentPickerValueConverter(IPublishedSnapshotAccessor publishedSnapshotAccessor)
        {
            _publishedSnapshotAccessor = publishedSnapshotAccessor;
        }

        public override bool IsConverter(PublishedPropertyType propertyType)
            => propertyType.PropertyEditorAlias.Equals(Constants.PropertyEditors.ContentPickerAlias)
            || propertyType.PropertyEditorAlias.Equals(Constants.PropertyEditors.ContentPicker2Alias);

        public override Type GetPropertyValueType(PublishedPropertyType propertyType)
            => typeof (IPublishedContent);

        public override PropertyCacheLevel GetPropertyCacheLevel(PublishedPropertyType propertyType)
            => PropertyCacheLevel.Elements;

        public override object ConvertSourceToInter(IPublishedElement owner, PublishedPropertyType propertyType, object source, bool preview)
        {
            if (source == null) return null;

            var attemptConvertInt = source.TryConvertTo<int>();
            if (attemptConvertInt.Success)
                return attemptConvertInt.Result;
            var attemptConvertUdi = source.TryConvertTo<Udi>();
            if (attemptConvertUdi.Success)
                return attemptConvertUdi.Result;
            return null;
        }

        public override object ConvertInterToObject(IPublishedElement owner, PublishedPropertyType propertyType, PropertyCacheLevel referenceCacheLevel, object inter, bool preview)
        {
            if (inter == null)
                return null;

            if ((propertyType.PropertyTypeAlias != null && PropertiesToExclude.Contains(propertyType.PropertyTypeAlias.ToLower(CultureInfo.InvariantCulture))) == false)
            {
                IPublishedContent content;
                if (inter is int id)
                {
                    content = _publishedSnapshotAccessor.PublishedSnapshot.Content.GetById(id);
                    if (content != null)
                        return content;
                }
                else
                {
                    var udi = inter as GuidUdi;
                    if (udi == null)
                        return null;
                    content = _publishedSnapshotAccessor.PublishedSnapshot.Content.GetById(udi.Guid);
                    if (content != null)
                        return content;
                }
            }

            return inter;
        }

        public override object ConvertInterToXPath(IPublishedElement owner, PublishedPropertyType propertyType, PropertyCacheLevel referenceCacheLevel, object inter, bool preview)
        {
            return inter.ToString();
        }
    }
}
