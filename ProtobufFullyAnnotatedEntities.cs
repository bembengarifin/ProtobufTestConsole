using System;
using ProtoBuf;

namespace ProtobufTestConsole.ProtobufFullyAnnotatedEntities
{
    [ProtoContract]
    [ProtoInclude(1, typeof(NamedEntity))]
    [ProtoInclude(2, typeof(PersonEntity))]
    public class EntityIdentifier
    {
        [ProtoMember(3)]
        public string Id { get; set; }
        public override string ToString()
        {
            return Id;
        }
    }

    [ProtoContract]
    public class NamedEntity : EntityIdentifier
    {
        [ProtoMember(1)]
        public string Name { get; set; }
        public override string ToString()
        {
            return Id + " - " + Name;
        }
    }

    [ProtoContract]
    public class PersonEntity : EntityIdentifier
    {
        [ProtoMember(1)]
        public string FirstName { get; set; }
        [ProtoMember(2)]
        public string LastName { get; set; }

        public string FullName
        {
            get { return FirstName + ", " + LastName; }
        }

        [ProtoMember(3)]
        public int Age { get; set; }

        [ProtoMember(4)]
        public Gender Gender { get; set; }

        public override string ToString()
        {
            return Id + "," + Gender + "," + FirstName + "," + LastName + "," + Age;
        }

        [ProtoMember(100)]
        public EntityIdentifier E1 { get; set; }
        [ProtoMember(101)]
        public EntityIdentifier E2 { get; set; }
        [ProtoMember(102)]
        public EntityIdentifier E3 { get; set; }
        [ProtoMember(103)]
        public EntityIdentifier E4 { get; set; }
        [ProtoMember(104)]
        public EntityIdentifier E5 { get; set; }
        [ProtoMember(105)]
        public EntityIdentifier E6 { get; set; }
        [ProtoMember(106)]
        public EntityIdentifier E7 { get; set; }
        [ProtoMember(107)]
        public EntityIdentifier E8 { get; set; }
        [ProtoMember(108)]
        public EntityIdentifier E9 { get; set; }
        [ProtoMember(109)]
        public EntityIdentifier E10 { get; set; }
        [ProtoMember(110)]
        public EntityIdentifier E11 { get; set; }
        [ProtoMember(111)]
        public EntityIdentifier E12 { get; set; }
        [ProtoMember(112)]
        public EntityIdentifier E13 { get; set; }
        [ProtoMember(113)]
        public EntityIdentifier E14 { get; set; }
        [ProtoMember(114)]
        public EntityIdentifier E15 { get; set; }
        [ProtoMember(115)]
        public EntityIdentifier E16 { get; set; }
        [ProtoMember(116)]
        public EntityIdentifier E17 { get; set; }
        [ProtoMember(117)]
        public EntityIdentifier E18 { get; set; }
        [ProtoMember(118)]
        public EntityIdentifier E19 { get; set; }
        [ProtoMember(119)]
        public EntityIdentifier E20 { get; set; }
        [ProtoMember(120)]
        public EntityIdentifier E21 { get; set; }
        [ProtoMember(121)]
        public EntityIdentifier E22 { get; set; }
        [ProtoMember(122)]
        public EntityIdentifier E23 { get; set; }
        [ProtoMember(123)]
        public EntityIdentifier E24 { get; set; }
        [ProtoMember(124)]
        public EntityIdentifier E25 { get; set; }
        [ProtoMember(125)]
        public EntityIdentifier E26 { get; set; }
        [ProtoMember(126)]
        public EntityIdentifier E27 { get; set; }
        [ProtoMember(127)]
        public EntityIdentifier E28 { get; set; }
        [ProtoMember(128)]
        public EntityIdentifier E29 { get; set; }
        [ProtoMember(129)]
        public EntityIdentifier E30 { get; set; }
        [ProtoMember(130)]
        public EntityIdentifier E31 { get; set; }
        [ProtoMember(131)]
        public EntityIdentifier E32 { get; set; }
        [ProtoMember(132)]
        public EntityIdentifier E33 { get; set; }
        [ProtoMember(133)]
        public EntityIdentifier E34 { get; set; }
        [ProtoMember(134)]
        public EntityIdentifier E35 { get; set; }
        [ProtoMember(135)]
        public EntityIdentifier E36 { get; set; }
        [ProtoMember(136)]
        public EntityIdentifier E37 { get; set; }
        [ProtoMember(137)]
        public EntityIdentifier E38 { get; set; }
        [ProtoMember(138)]
        public EntityIdentifier E39 { get; set; }
        [ProtoMember(139)]
        public EntityIdentifier E40 { get; set; }
        [ProtoMember(140)]
        public EntityIdentifier E41 { get; set; }
        [ProtoMember(141)]
        public EntityIdentifier E42 { get; set; }
        [ProtoMember(142)]
        public EntityIdentifier E43 { get; set; }
        [ProtoMember(143)]
        public EntityIdentifier E44 { get; set; }
        [ProtoMember(144)]
        public EntityIdentifier E45 { get; set; }
        [ProtoMember(145)]
        public EntityIdentifier E46 { get; set; }
        [ProtoMember(146)]
        public EntityIdentifier E47 { get; set; }
        [ProtoMember(147)]
        public EntityIdentifier E48 { get; set; }
        [ProtoMember(148)]
        public EntityIdentifier E49 { get; set; }
        [ProtoMember(149)]
        public EntityIdentifier E50 { get; set; }
        [ProtoMember(150)]
        public String S1 { get; set; }
        [ProtoMember(151)]
        public String S2 { get; set; }
        [ProtoMember(152)]
        public String S3 { get; set; }
        [ProtoMember(153)]
        public String S4 { get; set; }
        [ProtoMember(154)]
        public String S5 { get; set; }
        [ProtoMember(155)]
        public String S6 { get; set; }
        [ProtoMember(156)]
        public String S7 { get; set; }
        [ProtoMember(157)]
        public String S8 { get; set; }
        [ProtoMember(158)]
        public String S9 { get; set; }
        [ProtoMember(159)]
        public String S10 { get; set; }
        [ProtoMember(160)]
        public String S11 { get; set; }
        [ProtoMember(161)]
        public String S12 { get; set; }
        [ProtoMember(162)]
        public String S13 { get; set; }
        [ProtoMember(163)]
        public String S14 { get; set; }
        [ProtoMember(164)]
        public String S15 { get; set; }
        [ProtoMember(165)]
        public String S16 { get; set; }
        [ProtoMember(166)]
        public String S17 { get; set; }
        [ProtoMember(167)]
        public String S18 { get; set; }
        [ProtoMember(168)]
        public String S19 { get; set; }
        [ProtoMember(169)]
        public String S20 { get; set; }
        [ProtoMember(170)]
        public String S21 { get; set; }
        [ProtoMember(171)]
        public String S22 { get; set; }
        [ProtoMember(172)]
        public String S23 { get; set; }
        [ProtoMember(173)]
        public String S24 { get; set; }
        [ProtoMember(174)]
        public String S25 { get; set; }
        [ProtoMember(175)]
        public String S26 { get; set; }
        [ProtoMember(176)]
        public String S27 { get; set; }
        [ProtoMember(177)]
        public String S28 { get; set; }
        [ProtoMember(178)]
        public String S29 { get; set; }
        [ProtoMember(179)]
        public String S30 { get; set; }
        [ProtoMember(180)]
        public String S31 { get; set; }
        [ProtoMember(181)]
        public String S32 { get; set; }
        [ProtoMember(182)]
        public String S33 { get; set; }
        [ProtoMember(183)]
        public String S34 { get; set; }
        [ProtoMember(184)]
        public String S35 { get; set; }
        [ProtoMember(185)]
        public String S36 { get; set; }
        [ProtoMember(186)]
        public String S37 { get; set; }
        [ProtoMember(187)]
        public String S38 { get; set; }
        [ProtoMember(188)]
        public String S39 { get; set; }
        [ProtoMember(189)]
        public String S40 { get; set; }
        [ProtoMember(190)]
        public String S41 { get; set; }
        [ProtoMember(191)]
        public String S42 { get; set; }
        [ProtoMember(192)]
        public String S43 { get; set; }
        [ProtoMember(193)]
        public String S44 { get; set; }
        [ProtoMember(194)]
        public String S45 { get; set; }
        [ProtoMember(195)]
        public String S46 { get; set; }
        [ProtoMember(196)]
        public String S47 { get; set; }
        [ProtoMember(197)]
        public String S48 { get; set; }
        [ProtoMember(198)]
        public String S49 { get; set; }
        [ProtoMember(199)]
        public String S50 { get; set; }
        [ProtoMember(200)]
        public NamedEntity N1 { get; set; }
        [ProtoMember(201)]
        public NamedEntity N2 { get; set; }
        [ProtoMember(202)]
        public NamedEntity N3 { get; set; }
        [ProtoMember(203)]
        public NamedEntity N4 { get; set; }
        [ProtoMember(204)]
        public NamedEntity N5 { get; set; }
        [ProtoMember(205)]
        public NamedEntity N6 { get; set; }
        [ProtoMember(206)]
        public NamedEntity N7 { get; set; }
        [ProtoMember(207)]
        public NamedEntity N8 { get; set; }
        [ProtoMember(208)]
        public NamedEntity N9 { get; set; }
        [ProtoMember(209)]
        public NamedEntity N10 { get; set; }
        [ProtoMember(210)]
        public NamedEntity N11 { get; set; }
        [ProtoMember(211)]
        public NamedEntity N12 { get; set; }
        [ProtoMember(212)]
        public NamedEntity N13 { get; set; }
        [ProtoMember(213)]
        public NamedEntity N14 { get; set; }
        [ProtoMember(214)]
        public NamedEntity N15 { get; set; }
        [ProtoMember(215)]
        public NamedEntity N16 { get; set; }
        [ProtoMember(216)]
        public NamedEntity N17 { get; set; }
        [ProtoMember(217)]
        public NamedEntity N18 { get; set; }
        [ProtoMember(218)]
        public NamedEntity N19 { get; set; }
        [ProtoMember(219)]
        public NamedEntity N20 { get; set; }
        [ProtoMember(220)]
        public NamedEntity N21 { get; set; }
        [ProtoMember(221)]
        public NamedEntity N22 { get; set; }
        [ProtoMember(222)]
        public NamedEntity N23 { get; set; }
        [ProtoMember(223)]
        public NamedEntity N24 { get; set; }
        [ProtoMember(224)]
        public NamedEntity N25 { get; set; }
        [ProtoMember(225)]
        public NamedEntity N26 { get; set; }
        [ProtoMember(226)]
        public NamedEntity N27 { get; set; }
        [ProtoMember(227)]
        public NamedEntity N28 { get; set; }
        [ProtoMember(228)]
        public NamedEntity N29 { get; set; }
        [ProtoMember(229)]
        public NamedEntity N30 { get; set; }
        [ProtoMember(230)]
        public NamedEntity N31 { get; set; }
        [ProtoMember(231)]
        public NamedEntity N32 { get; set; }
        [ProtoMember(232)]
        public NamedEntity N33 { get; set; }
        [ProtoMember(233)]
        public NamedEntity N34 { get; set; }
        [ProtoMember(234)]
        public NamedEntity N35 { get; set; }
        [ProtoMember(235)]
        public NamedEntity N36 { get; set; }
        [ProtoMember(236)]
        public NamedEntity N37 { get; set; }
        [ProtoMember(237)]
        public NamedEntity N38 { get; set; }
        [ProtoMember(238)]
        public NamedEntity N39 { get; set; }
        [ProtoMember(239)]
        public NamedEntity N40 { get; set; }
        [ProtoMember(240)]
        public NamedEntity N41 { get; set; }
        [ProtoMember(241)]
        public NamedEntity N42 { get; set; }
        [ProtoMember(242)]
        public NamedEntity N43 { get; set; }
        [ProtoMember(243)]
        public NamedEntity N44 { get; set; }
        [ProtoMember(244)]
        public NamedEntity N45 { get; set; }
        [ProtoMember(245)]
        public NamedEntity N46 { get; set; }
        [ProtoMember(246)]
        public NamedEntity N47 { get; set; }
        [ProtoMember(247)]
        public NamedEntity N48 { get; set; }
        [ProtoMember(248)]
        public NamedEntity N49 { get; set; }
        [ProtoMember(249)]
        public NamedEntity N50 { get; set; }
    }
}
