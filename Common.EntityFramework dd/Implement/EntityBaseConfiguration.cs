using System;
using Common.EntityFramework.Interfaces;

namespace Common.EntityFramework.Implement
{
    public abstract class EntityBaseConfiguration<T> where T : class, IEntityBase
    {
        public void EntityConfiguration(EntityTypeBuilder<T> entityBuilder)
        {
            if (entityBuilder == null)
                throw new ArgumentNullException(nameof(entityBuilder));

            entityBuilder.HasKey(entity => entity.Id);

            entityBuilder.Property(entity => entity.Id)
                .HasColumnType("UNIQUEIDENTIFIER")
                .HasColumnName("Id")
                .ValueGeneratedOnAdd()
                .IsRequired();

            Configuration(entityBuilder);
        }

        protected abstract void Configuration(EntityTypeBuilder<T> entityBuilder);
    }
}