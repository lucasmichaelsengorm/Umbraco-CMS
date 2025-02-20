﻿using Umbraco.Cms.Core.Models.PublishedContent;

namespace Umbraco.Cms.Core.PublishedCache.Internal
{
    public class InternalPublishedProperty : IPublishedProperty
    {
        public IPublishedPropertyType PropertyType { get; set; }

        public string Alias { get; set; }

        public object SolidSourceValue { get; set; }

        public object SolidValue { get; set; }

        public bool SolidHasValue { get; set; }

        public object SolidXPathValue { get; set; }

        public virtual object GetSourceValue(string culture = null, string segment = null) => SolidSourceValue;

        public virtual object GetValue(string culture = null, string segment = null) => SolidValue;

        public virtual object GetXPathValue(string culture = null, string segment = null) => SolidXPathValue;

        public virtual bool HasValue(string culture = null, string segment = null) => SolidHasValue;
    }
}
