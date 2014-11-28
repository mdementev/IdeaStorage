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

    public sealed class TagManager : ITagManager
    {
        private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public Tag GetTagById(int id)
        {
            using (IdeaStorageEntities context = new IdeaStorageEntities())
            {
                Tag tag = context.TAGS.SingleOrDefault(u => u.TagId == id )
                    .ToModel();

                if (tag == null)
                {
                    string message = string.Format("Tag with id:'{0}' doesn't exist in data base.", id);
                    Log.Debug(message);
                    throw new EntityDoesNotExistException(message);
                }

                Log.DebugFormat("Returned: '{0}'", tag);

                return tag;
            }
        }

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
