using System;
using ProtoBuf;

namespace ProtobufTestConsole.ProtobufImplicitAnnotatedEntities
{
    [ProtoContract(ImplicitFields = ImplicitFields.AllPublic)]
    [ProtoInclude(10, typeof(NamedEntity))]
    [ProtoInclude(11, typeof(PersonEntity))]
    public class EntityIdentifier 
    {
        public string Id { get; set; }
        public override string ToString()
        {
            return Id;
        }
    }

    [ProtoContract(ImplicitFields = ImplicitFields.AllPublic)]
    public class NamedEntity : EntityIdentifier
    {
        public string Name { get; set; }
        public override string ToString()
        {
            return Id + " - " + Name;
        }
    }

    [ProtoContract(ImplicitFields = ImplicitFields.AllPublic)]
    public class PersonEntity : EntityIdentifier
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

        [ProtoIgnore]
        public string FullName
        {
            get { return FirstName + ", " + LastName; }
        }

        public int Age { get; set; }

        public Gender Gender { get; set; }

        public override string ToString()
        {
            return Id + "," + Gender + "," + FirstName + "," + LastName + "," + Age;
        }

        public EntityIdentifier E1 { get; set; }
        public EntityIdentifier E2 { get; set; }
        public EntityIdentifier E3 { get; set; }
        public EntityIdentifier E4 { get; set; }
        public EntityIdentifier E5 { get; set; }
        public EntityIdentifier E6 { get; set; }
        public EntityIdentifier E7 { get; set; }
        public EntityIdentifier E8 { get; set; }
        public EntityIdentifier E9 { get; set; }
        public EntityIdentifier E10 { get; set; }
        public EntityIdentifier E11 { get; set; }
        public EntityIdentifier E12 { get; set; }
        public EntityIdentifier E13 { get; set; }
        public EntityIdentifier E14 { get; set; }
        public EntityIdentifier E15 { get; set; }
        public EntityIdentifier E16 { get; set; }
        public EntityIdentifier E17 { get; set; }
        public EntityIdentifier E18 { get; set; }
        public EntityIdentifier E19 { get; set; }
        public EntityIdentifier E20 { get; set; }
        public EntityIdentifier E21 { get; set; }
        public EntityIdentifier E22 { get; set; }
        public EntityIdentifier E23 { get; set; }
        public EntityIdentifier E24 { get; set; }
        public EntityIdentifier E25 { get; set; }
        public EntityIdentifier E26 { get; set; }
        public EntityIdentifier E27 { get; set; }
        public EntityIdentifier E28 { get; set; }
        public EntityIdentifier E29 { get; set; }
        public EntityIdentifier E30 { get; set; }
        public EntityIdentifier E31 { get; set; }
        public EntityIdentifier E32 { get; set; }
        public EntityIdentifier E33 { get; set; }
        public EntityIdentifier E34 { get; set; }
        public EntityIdentifier E35 { get; set; }
        public EntityIdentifier E36 { get; set; }
        public EntityIdentifier E37 { get; set; }
        public EntityIdentifier E38 { get; set; }
        public EntityIdentifier E39 { get; set; }
        public EntityIdentifier E40 { get; set; }
        public EntityIdentifier E41 { get; set; }
        public EntityIdentifier E42 { get; set; }
        public EntityIdentifier E43 { get; set; }
        public EntityIdentifier E44 { get; set; }
        public EntityIdentifier E45 { get; set; }
        public EntityIdentifier E46 { get; set; }
        public EntityIdentifier E47 { get; set; }
        public EntityIdentifier E48 { get; set; }
        public EntityIdentifier E49 { get; set; }
        public EntityIdentifier E50 { get; set; }
        public String S1 { get; set; }
        public String S2 { get; set; }
        public String S3 { get; set; }
        public String S4 { get; set; }
        public String S5 { get; set; }
        public String S6 { get; set; }
        public String S7 { get; set; }
        public String S8 { get; set; }
        public String S9 { get; set; }
        public String S10 { get; set; }
        public String S11 { get; set; }
        public String S12 { get; set; }
        public String S13 { get; set; }
        public String S14 { get; set; }
        public String S15 { get; set; }
        public String S16 { get; set; }
        public String S17 { get; set; }
        public String S18 { get; set; }
        public String S19 { get; set; }
        public String S20 { get; set; }
        public String S21 { get; set; }
        public String S22 { get; set; }
        public String S23 { get; set; }
        public String S24 { get; set; }
        public String S25 { get; set; }
        public String S26 { get; set; }
        public String S27 { get; set; }
        public String S28 { get; set; }
        public String S29 { get; set; }
        public String S30 { get; set; }
        public String S31 { get; set; }
        public String S32 { get; set; }
        public String S33 { get; set; }
        public String S34 { get; set; }
        public String S35 { get; set; }
        public String S36 { get; set; }
        public String S37 { get; set; }
        public String S38 { get; set; }
        public String S39 { get; set; }
        public String S40 { get; set; }
        public String S41 { get; set; }
        public String S42 { get; set; }
        public String S43 { get; set; }
        public String S44 { get; set; }
        public String S45 { get; set; }
        public String S46 { get; set; }
        public String S47 { get; set; }
        public String S48 { get; set; }
        public String S49 { get; set; }
        public String S50 { get; set; }
        public NamedEntity N1 { get; set; }
        public NamedEntity N2 { get; set; }
        public NamedEntity N3 { get; set; }
        public NamedEntity N4 { get; set; }
        public NamedEntity N5 { get; set; }
        public NamedEntity N6 { get; set; }
        public NamedEntity N7 { get; set; }
        public NamedEntity N8 { get; set; }
        public NamedEntity N9 { get; set; }
        public NamedEntity N10 { get; set; }
        public NamedEntity N11 { get; set; }
        public NamedEntity N12 { get; set; }
        public NamedEntity N13 { get; set; }
        public NamedEntity N14 { get; set; }
        public NamedEntity N15 { get; set; }
        public NamedEntity N16 { get; set; }
        public NamedEntity N17 { get; set; }
        public NamedEntity N18 { get; set; }
        public NamedEntity N19 { get; set; }
        public NamedEntity N20 { get; set; }
        public NamedEntity N21 { get; set; }
        public NamedEntity N22 { get; set; }
        public NamedEntity N23 { get; set; }
        public NamedEntity N24 { get; set; }
        public NamedEntity N25 { get; set; }
        public NamedEntity N26 { get; set; }
        public NamedEntity N27 { get; set; }
        public NamedEntity N28 { get; set; }
        public NamedEntity N29 { get; set; }
        public NamedEntity N30 { get; set; }
        public NamedEntity N31 { get; set; }
        public NamedEntity N32 { get; set; }
        public NamedEntity N33 { get; set; }
        public NamedEntity N34 { get; set; }
        public NamedEntity N35 { get; set; }
        public NamedEntity N36 { get; set; }
        public NamedEntity N37 { get; set; }
        public NamedEntity N38 { get; set; }
        public NamedEntity N39 { get; set; }
        public NamedEntity N40 { get; set; }
        public NamedEntity N41 { get; set; }
        public NamedEntity N42 { get; set; }
        public NamedEntity N43 { get; set; }
        public NamedEntity N44 { get; set; }
        public NamedEntity N45 { get; set; }
        public NamedEntity N46 { get; set; }
        public NamedEntity N47 { get; set; }
        public NamedEntity N48 { get; set; }
        public NamedEntity N49 { get; set; }
        public NamedEntity N50 { get; set; }
    }
}
