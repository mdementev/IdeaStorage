namespace BusinessLogic.EntityManagers
{
    using System;
    using System.Linq;
    using System.Reflection;

    using BusinessLogic.Exceptions;
    using BusinessLogic.Mappers;
    using BusinessLogic.Validators;

    using IdeaSorage.DataModel;

    using IdeaStorage.EntriesModel.Entries;

    using log4net;

    public sealed class TagManager 
    {
        private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public Tag CreateTag(Tag tag)
        {
            Tag foundTag = this.FindTagByName(tag.Name);
            if (foundTag != null)
            {
                return foundTag;
            }

            using (var context = new IdeaStorageEntities())
            {
                TAG newTag = new TAG
                {
                    Name = tag.Name,
                };

                context.TAGS.Add(newTag);
                context.SaveChanges();

                return newTag.ToModel();
            }
        }

        public Tag FindTagByName(string name)
        {
            using (var context = new IdeaStorageEntities())
            {
                Tag tag = context.TAGS.SingleOrDefault(t => t.Name.Equals(name)).ToModel();
                Log.DebugFormat("Found tag:'{0}'", tag);
                return tag;
            }
        }


    }
}
