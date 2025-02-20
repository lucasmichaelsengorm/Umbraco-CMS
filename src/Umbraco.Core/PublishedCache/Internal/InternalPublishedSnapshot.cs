using System;
using Umbraco.Cms.Core.Cache;

namespace Umbraco.Cms.Core.PublishedCache.Internal
{
    public sealed class InternalPublishedSnapshot : IPublishedSnapshot
    {
        public InternalPublishedContentCache InnerContentCache { get; } = new InternalPublishedContentCache();
        public InternalPublishedContentCache InnerMediaCache { get; } = new InternalPublishedContentCache();

        public IPublishedContentCache Content => InnerContentCache;

        public IPublishedMediaCache Media => InnerMediaCache;

        public IPublishedMemberCache Members => null;

        public IDomainCache Domains => null;

        public IDisposable ForcedPreview(bool forcedPreview, Action<bool> callback = null) => throw new NotImplementedException();

        public void Resync()
        {
        }

        public IAppCache SnapshotCache => null;

        public IAppCache ElementsCache => null;

        public void Dispose()
        {
        }
    }
}
