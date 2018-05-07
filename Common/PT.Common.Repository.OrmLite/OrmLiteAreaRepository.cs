using ServiceStack;
using ServiceStack.Auth;
using ServiceStack.Data;
using ServiceStack.OrmLite;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PT.Common.Repository.OrmLite
{
    public class OrmLiteAreaRepository : OrmLiteAreaRepository<Area>
    {
        public OrmLiteAreaRepository(IDbConnectionFactory dbFactory) : base(dbFactory) { }
        public OrmLiteAreaRepository(IDbConnectionFactory dbFactory, string namedConnnection = null)
            : base(dbFactory, namedConnnection) { }
    }

    public class OrmLiteAreaRepository<TArea> : OrmLiteBaseRepository, IAreaRepository, IRequiresSchema, IClearable
       where TArea : class, IArea
    {
        protected OrmLiteAreaRepository(IDbConnectionFactory dbFactory, string namedConnnection = null)
            : base(dbFactory, namedConnnection)
        {

        }

        public void Clear()
        {
            Exec(db =>
            {
                db.DeleteAll<TArea>();
            });
        }

        public IArea CreateArea(IArea newArea)
        {
            newArea.ValidateNewArea();
            return Exec(db =>
            {
                newArea.IsPrgCreatedDate = DateTime.Now;
                newArea.IsPrgUpdatedDate = newArea.IsPrgCreatedDate;

                db.Save((TArea)newArea);

                newArea = db.SingleById<TArea>(newArea.Id);
                return newArea;
            });
        }

        public void DeleteArea(int id)
        {
            Exec(db =>
            {
                using (var trans = db.OpenTransaction())
                {
                    var area = GetArea(id);
                    area.Status = 0;
                    area.IsPrgUpdatedDate = DateTime.Now;
                    db.Save((TArea)area);

                    trans.Commit();
                }
            });
        }

        public IArea GetArea(int id)
        {
            return Exec(db =>
            {
                return db.Select<TArea>(q => q.Id == id).FirstOrDefault();
            });
        }

        public List<IArea> GetAreaByBaseId(int baseId)
        {
            return Exec(db =>
            {
                return db.Select<TArea>(q => q.BaseId == baseId).OrderBy(c => c.Name).Select(c => (IArea)c).ToList();
            });
        }

        public List<IArea> GetAreaByType(int type)
        {
            return Exec(db =>
            {
                return db.Select<TArea>(q => q.Type == type).OrderBy(c => c.Name).Select(c => (IArea)c).ToList();
            });
        }

        public void InitSchema()
        {
            Exec(db =>
            {
                db.CreateTableIfNotExists<TArea>();
            });
        }

        public virtual void DropAndReCreateTables()
        {
            Exec(db =>
            {
                db.DropAndCreateTable<TArea>();
            });
        }

        public IArea UpdateArea(IArea existingArea, IArea newArea)
        {
            newArea.ValidateNewArea();

            return Exec(db =>
            {
                newArea.Id = existingArea.Id;
                newArea.IsPrgCreatedDate = existingArea.IsPrgCreatedDate;
                newArea.IsPrgUpdatedDate = DateTime.Now;

                db.Save((TArea)newArea);

                return newArea;
            });
        }

        public IArea GetArea(string name, int type)
        {
            return Exec(db =>
            {
                return db.Select<TArea>(q => (q.Name.ToLower() == name.ToLower()) && q.Type == type).Select(c => (IArea)c).FirstOrDefault();
            });
        }
    }
}
