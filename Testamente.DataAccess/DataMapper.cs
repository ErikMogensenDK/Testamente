// namespace Testamente.DataAccess;
// using Testamente.Domain;

// public static class PersonMapper
// {
//     public static PersonEntity ToEntity(Person dto, int? parentId = null)
//     {
//         return new PersonEntity
//         {
//             PersonEntityId = dto.PersonId,
//             Name = dto.Name,
//             BirthDate = dto.BirthDate,
//             Address = dto.Address,
//             IsAlive = dto.IsAlive,
//             ParentId = parentId
//         };
//     }

//     public static Person ToDomainObject(PersonEntity entity, List<Person> children)
//     {
//         return new Person
//         {
//             PersonId = entity.PersonEntityId,
//             Name = entity.Name,
//             BirthDate = entity.BirthDate,
//             Address = entity.Address,
//             IsAlive = entity.IsAlive,
//             Children = children
//         };
//     }

// 	public static List<PersonEntity> FlattenPersonHierarchy(Person root)
// 	{
// 		var entities = new List<PersonEntity>();
// 		FlattenPersonRecursive(root, null, entities);
// 		return entities;
// 	}

// 	private static void FlattenPersonRecursive(Person person, int? parentId, List<PersonEntity> entities)
// 	{
// 		var entity = PersonMapper.ToEntity(person, parentId);
// 		entities.Add(entity);

// 		foreach (var child in person.Children ?? Enumerable.Empty<Person>())
// 		{
// 			FlattenPersonRecursive(child, entity.PersonEntityId, entities);
// 		}
// 	}
// 	public static Person ReconstructPersonHierarchy(List<PersonEntity> entities)
// 	{
// 		var personDtos = entities.Select(e => PersonMapper.ToDomainObject(e, new List<Person>())).ToList();
// 		var dtoMap = personDtos.ToDictionary(p => p.PersonId);

// 		foreach (var entity in entities.Where(e => e.ParentId.HasValue))
// 		{
// 			var parentDto = dtoMap[entity.ParentId.Value];
// 			parentDto.Children.Add(dtoMap[entity.PersonEntityId]);
// 		}

// 		return personDtos.First(p => !entities.Any(e => e.PersonEntityId == p.PersonId && e.ParentId.HasValue));
// 	}

// }
