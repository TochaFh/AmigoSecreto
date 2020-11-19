namespace AmigoSecreto.Models
{
    public class Person
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int SecretFriendId { get; set; } = -1;

        public bool SecretFriendWasRevealed { get; set; } = false;

        #region Constructors

        public Person()
        {

        }

        public Person(int id, string name)
        {
            Id = id;
            Name = name;
        }

        #endregion

        public BasicPerson ToBasic() => new BasicPerson(this);

        public override string ToString() => Name;
    }

    public class BasicPerson
    {
        /// <summary>
        /// Não é recomendado mudar o id.
        /// </summary>
        public int Id { get; }

        public string Name { get; set; }

        public bool SecretFriendWasRevealed { get; }

        public BasicPerson(string name)
        {
            Name = name;
        }

        public BasicPerson(Person person)
        {
            Id = person.Id;
            Name = person.Name;
            SecretFriendWasRevealed = person.SecretFriendWasRevealed;
        }

        public Person ToPerson(int id) => new Person(id, Name);

        public override string ToString()
        {
            return Name;
        }

        public static implicit operator BasicPerson(string name) => new BasicPerson(name);
    }
}
