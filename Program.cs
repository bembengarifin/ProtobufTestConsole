using ProtoBuf;
using ProtoBuf.Meta;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace ProtobufTestConsole
{
    public enum Gender
    {
        Male,
        Female
    }

    class Program
    {
        //http://stackoverflow.com/questions/1122483/c-sharp-random-string-generator
        private static Random random = new Random((int)DateTime.Now.Ticks);//thanks to McAden
        private string RandomString(int size)
        {
            StringBuilder builder = new StringBuilder();
            char ch;
            for (int i = 0; i < size; i++)
            {
                ch = Convert.ToChar(Convert.ToInt32(Math.Floor(26 * random.NextDouble() + 65)));
                builder.Append(ch);
            }

            return builder.ToString();
        }

        static void Main(string[] args)
        {
            var p = new Program();
            var noOfItems = new [] { 100, 100, 100, 1000, 1000, 10000, 10000 }; // first one is to warm up
            
            //p.TestBinaryEntities(noOfItems);
            p.TestProtobufFullyAnnotatedEntities(noOfItems);
            p.TestProtobufImplicitAnnotatedEntities(noOfItems);
            p.TestProtobufRuntimeRegistration(noOfItems);
            p.TestProtobufRuntimeRegistrationWithCompiledTypeModel(noOfItems);
            p.TestProtobufPreCompiledTypeModel(noOfItems);

            //Enumerable.Range(1, 50).ToList().ForEach(x =>
            //    {
            //        Debug.WriteLine("IEntityIdentifier E{0} {{ get; set; }}", x, string.Empty);
            //    });

            //Enumerable.Range(1, 50).ToList().ForEach(x =>
            //{
            //    Debug.WriteLine("String S{0} {{ get; set; }}", x, string.Empty);
            //});

            //Enumerable.Range(1, 50).ToList().ForEach(x =>
            //{
            //    Debug.WriteLine("INamedEntity N{0} {{ get; set; }}", x, string.Empty);
            //});

            //var annotationCounter = 100;

            //Enumerable.Range(1, 50).ToList().ForEach(x =>
            //{
            //    //Debug.WriteLine("[ProtoMember({0})]", annotationCounter++);
            //    //Debug.WriteLine("public EntityIdentifier E{0} {{ get; set; }}", x, string.Empty);
            //    Debug.WriteLine("p.E{0} = createIdentifier();", x, string.Empty);
            //});

            //Enumerable.Range(1, 50).ToList().ForEach(x =>
            //{
            //    //Debug.WriteLine("[ProtoMember({0})]", annotationCounter++);
            //    //Debug.WriteLine("public String S{0} {{ get; set; }}", x, string.Empty);
            //    Debug.WriteLine("p.S{0} = RandomString(10);", x, string.Empty);
            //});

            //Enumerable.Range(1, 50).ToList().ForEach(x =>
            //{
            //    //Debug.WriteLine("[ProtoMember({0})]", annotationCounter++);
            //    //Debug.WriteLine("public NamedEntity N{0} {{ get; set; }}", x, string.Empty);
            //    Debug.WriteLine("p.N{0} = createIdentifier();", x, string.Empty);
            //});

            Debug.WriteLine("program is completed");
            Console.Read();
        }

        void Test<T>(int noOfItems, Action<Stream, IList<T>> serialize, Func<Stream, IList<T>> deserialize, Func<T> createRow)
        {
            using (var ms = new MemoryStream())
            {
                //Debug.WriteLine("create {0} persons", noOfItems);
                var sw = Stopwatch.StartNew();
                var persons = Enumerable.Range(1, noOfItems).Select(x => createRow()).ToList();

                //Debug.WriteLine("serialize - {0}ms", sw.ElapsedMilliseconds);
                var swSerialize = Stopwatch.StartNew();
                serialize(ms, persons);
                swSerialize.Stop();

                var memorySize = ms.Length;
                //Debug.WriteLine("length: {0} - {1}ms", memorySize, sw.ElapsedMilliseconds);

                //Debug.WriteLine("reset position - {0}ms", sw.ElapsedMilliseconds);
                ms.Position = 0;

                //Debug.WriteLine("deserialized - {0}ms", sw.ElapsedMilliseconds);
                var swDeserialize = Stopwatch.StartNew();
                var deserialized = deserialize(ms);
                swDeserialize.Stop();

                //var t = typeof(T);
                //var firstItem = deserialized[0];
                //var props = t.GetProperties(BindingFlags.Instance | BindingFlags.Public);
                //foreach (var p in props)
                //{
                //    var val = p.GetValue(firstItem);
                //    Debug.WriteLine("{0}: {1}", p.Name, val);
                //    Debug.Assert(p != null);
                //}

                //foreach (var item in deserialized)
                //{
                //    Debug.WriteLine(item);
                //}
                //Debug.WriteLine("completed: {0} - {1}ms", deserialized.Count, sw.ElapsedMilliseconds);

                Debug.WriteLine("Process: {0} items, MemorySize: {2}, Completed in: {1} ms, Serialization took: {3} ms, Deserialization took: {4} ms", noOfItems, sw.ElapsedMilliseconds, memorySize, swSerialize.ElapsedMilliseconds, swDeserialize.ElapsedMilliseconds);
            }
        }

        void TestBinaryEntities(int[] noOfItems)
        {
            Debug.WriteLine("------------------");
            Debug.WriteLine("TestBinaryEntities");
            Debug.WriteLine("------------------");

            Func<BinaryEntities.NamedEntity> createNamedEntity = () => new BinaryEntities.NamedEntity { Id = RandomString(5), Name = RandomString(10) };
            Func<BinaryEntities.EntityIdentifier> createIdentifier = () => new BinaryEntities.EntityIdentifier { Id = RandomString(5) };

            for (int i = 0; i < noOfItems.Length; i++)
            {
                BinaryFormatter binaryFormatter = binaryFormatter = new BinaryFormatter();
                Test(noOfItems[i], (m, l) => binaryFormatter.Serialize(m, l), (m) => binaryFormatter.Deserialize(m) as List<BinaryEntities.PersonEntity>,
                    () =>
                    {
                        var p = new BinaryEntities.PersonEntity();
                        p.Id = RandomString(5);
                        p.Gender = Gender.Male;
                        p.FirstName = RandomString(10);
                        p.LastName = RandomString(10);
                        p.Age = 10;
                        p.E1 = createIdentifier();
                        p.E2 = createIdentifier();
                        p.E3 = createIdentifier();
                        p.E4 = createIdentifier();
                        p.E5 = createIdentifier();
                        p.E6 = createIdentifier();
                        p.E7 = createIdentifier();
                        p.E8 = createIdentifier();
                        p.E9 = createIdentifier();
                        p.E10 = createIdentifier();
                        p.E11 = createIdentifier();
                        p.E12 = createIdentifier();
                        p.E13 = createIdentifier();
                        p.E14 = createIdentifier();
                        p.E15 = createIdentifier();
                        p.E16 = createIdentifier();
                        p.E17 = createIdentifier();
                        p.E18 = createIdentifier();
                        p.E19 = createIdentifier();
                        p.E20 = createIdentifier();
                        p.E21 = createIdentifier();
                        p.E22 = createIdentifier();
                        p.E23 = createIdentifier();
                        p.E24 = createIdentifier();
                        p.E25 = createIdentifier();
                        p.E26 = createIdentifier();
                        p.E27 = createIdentifier();
                        p.E28 = createIdentifier();
                        p.E29 = createIdentifier();
                        p.E30 = createIdentifier();
                        p.E31 = createIdentifier();
                        p.E32 = createIdentifier();
                        p.E33 = createIdentifier();
                        p.E34 = createIdentifier();
                        p.E35 = createIdentifier();
                        p.E36 = createIdentifier();
                        p.E37 = createIdentifier();
                        p.E38 = createIdentifier();
                        p.E39 = createIdentifier();
                        p.E40 = createIdentifier();
                        p.E41 = createIdentifier();
                        p.E42 = createIdentifier();
                        p.E43 = createIdentifier();
                        p.E44 = createIdentifier();
                        p.E45 = createIdentifier();
                        p.E46 = createIdentifier();
                        p.E47 = createIdentifier();
                        p.E48 = createIdentifier();
                        p.E49 = createIdentifier();
                        p.E50 = createIdentifier();
                        p.S1 = RandomString(10);
                        p.S2 = RandomString(10);
                        p.S3 = RandomString(10);
                        p.S4 = RandomString(10);
                        p.S5 = RandomString(10);
                        p.S6 = RandomString(10);
                        p.S7 = RandomString(10);
                        p.S8 = RandomString(10);
                        p.S9 = RandomString(10);
                        p.S10 = RandomString(10);
                        p.S11 = RandomString(10);
                        p.S12 = RandomString(10);
                        p.S13 = RandomString(10);
                        p.S14 = RandomString(10);
                        p.S15 = RandomString(10);
                        p.S16 = RandomString(10);
                        p.S17 = RandomString(10);
                        p.S18 = RandomString(10);
                        p.S19 = RandomString(10);
                        p.S20 = RandomString(10);
                        p.S21 = RandomString(10);
                        p.S22 = RandomString(10);
                        p.S23 = RandomString(10);
                        p.S24 = RandomString(10);
                        p.S25 = RandomString(10);
                        p.S26 = RandomString(10);
                        p.S27 = RandomString(10);
                        p.S28 = RandomString(10);
                        p.S29 = RandomString(10);
                        p.S30 = RandomString(10);
                        p.S31 = RandomString(10);
                        p.S32 = RandomString(10);
                        p.S33 = RandomString(10);
                        p.S34 = RandomString(10);
                        p.S35 = RandomString(10);
                        p.S36 = RandomString(10);
                        p.S37 = RandomString(10);
                        p.S38 = RandomString(10);
                        p.S39 = RandomString(10);
                        p.S40 = RandomString(10);
                        p.S41 = RandomString(10);
                        p.S42 = RandomString(10);
                        p.S43 = RandomString(10);
                        p.S44 = RandomString(10);
                        p.S45 = RandomString(10);
                        p.S46 = RandomString(10);
                        p.S47 = RandomString(10);
                        p.S48 = RandomString(10);
                        p.S49 = RandomString(10);
                        p.S50 = RandomString(10);
                        p.N1 = createNamedEntity();
                        p.N2 = createNamedEntity();
                        p.N3 = createNamedEntity();
                        p.N4 = createNamedEntity();
                        p.N5 = createNamedEntity();
                        p.N6 = createNamedEntity();
                        p.N7 = createNamedEntity();
                        p.N8 = createNamedEntity();
                        p.N9 = createNamedEntity();
                        p.N10 = createNamedEntity();
                        p.N11 = createNamedEntity();
                        p.N12 = createNamedEntity();
                        p.N13 = createNamedEntity();
                        p.N14 = createNamedEntity();
                        p.N15 = createNamedEntity();
                        p.N16 = createNamedEntity();
                        p.N17 = createNamedEntity();
                        p.N18 = createNamedEntity();
                        p.N19 = createNamedEntity();
                        p.N20 = createNamedEntity();
                        p.N21 = createNamedEntity();
                        p.N22 = createNamedEntity();
                        p.N23 = createNamedEntity();
                        p.N24 = createNamedEntity();
                        p.N25 = createNamedEntity();
                        p.N26 = createNamedEntity();
                        p.N27 = createNamedEntity();
                        p.N28 = createNamedEntity();
                        p.N29 = createNamedEntity();
                        p.N30 = createNamedEntity();
                        p.N31 = createNamedEntity();
                        p.N32 = createNamedEntity();
                        p.N33 = createNamedEntity();
                        p.N34 = createNamedEntity();
                        p.N35 = createNamedEntity();
                        p.N36 = createNamedEntity();
                        p.N37 = createNamedEntity();
                        p.N38 = createNamedEntity();
                        p.N39 = createNamedEntity();
                        p.N40 = createNamedEntity();
                        p.N41 = createNamedEntity();
                        p.N42 = createNamedEntity();
                        p.N43 = createNamedEntity();
                        p.N44 = createNamedEntity();
                        p.N45 = createNamedEntity();
                        p.N46 = createNamedEntity();
                        p.N47 = createNamedEntity();
                        p.N48 = createNamedEntity();
                        p.N49 = createNamedEntity();
                        p.N50 = createNamedEntity();
                        return p;
                    });
            }
        }

        void TestProtobufFullyAnnotatedEntities(int[] noOfItems)
        {
            Debug.WriteLine("----------------------------------");
            Debug.WriteLine("TestProtobufFullyAnnotatedEntities");
            Debug.WriteLine("----------------------------------");

            Func<ProtobufFullyAnnotatedEntities.NamedEntity> createNamedEntity = () => new ProtobufFullyAnnotatedEntities.NamedEntity { Id = RandomString(5), Name = RandomString(10) };
            Func<ProtobufFullyAnnotatedEntities.EntityIdentifier> createIdentifier = () => new ProtobufFullyAnnotatedEntities.EntityIdentifier { Id = RandomString(5) };

            for (int i = 0; i < noOfItems.Length; i++)
            {
                Test(noOfItems[i], (m, l) => Serializer.Serialize(m, l), (m) => Serializer.Deserialize<List<ProtobufFullyAnnotatedEntities.PersonEntity>>(m),
                    () =>
                    {
                        var p = new ProtobufFullyAnnotatedEntities.PersonEntity();
                        p.Id = RandomString(5);
                        p.Gender = Gender.Male;
                        p.FirstName = RandomString(10);
                        p.LastName = RandomString(10);
                        p.Age = 10;
                        p.E1 = createIdentifier();
                        p.E2 = createIdentifier();
                        p.E3 = createIdentifier();
                        p.E4 = createIdentifier();
                        p.E5 = createIdentifier();
                        p.E6 = createIdentifier();
                        p.E7 = createIdentifier();
                        p.E8 = createIdentifier();
                        p.E9 = createIdentifier();
                        p.E10 = createIdentifier();
                        p.E11 = createIdentifier();
                        p.E12 = createIdentifier();
                        p.E13 = createIdentifier();
                        p.E14 = createIdentifier();
                        p.E15 = createIdentifier();
                        p.E16 = createIdentifier();
                        p.E17 = createIdentifier();
                        p.E18 = createIdentifier();
                        p.E19 = createIdentifier();
                        p.E20 = createIdentifier();
                        p.E21 = createIdentifier();
                        p.E22 = createIdentifier();
                        p.E23 = createIdentifier();
                        p.E24 = createIdentifier();
                        p.E25 = createIdentifier();
                        p.E26 = createIdentifier();
                        p.E27 = createIdentifier();
                        p.E28 = createIdentifier();
                        p.E29 = createIdentifier();
                        p.E30 = createIdentifier();
                        p.E31 = createIdentifier();
                        p.E32 = createIdentifier();
                        p.E33 = createIdentifier();
                        p.E34 = createIdentifier();
                        p.E35 = createIdentifier();
                        p.E36 = createIdentifier();
                        p.E37 = createIdentifier();
                        p.E38 = createIdentifier();
                        p.E39 = createIdentifier();
                        p.E40 = createIdentifier();
                        p.E41 = createIdentifier();
                        p.E42 = createIdentifier();
                        p.E43 = createIdentifier();
                        p.E44 = createIdentifier();
                        p.E45 = createIdentifier();
                        p.E46 = createIdentifier();
                        p.E47 = createIdentifier();
                        p.E48 = createIdentifier();
                        p.E49 = createIdentifier();
                        p.E50 = createIdentifier();
                        p.S1 = RandomString(10);
                        p.S2 = RandomString(10);
                        p.S3 = RandomString(10);
                        p.S4 = RandomString(10);
                        p.S5 = RandomString(10);
                        p.S6 = RandomString(10);
                        p.S7 = RandomString(10);
                        p.S8 = RandomString(10);
                        p.S9 = RandomString(10);
                        p.S10 = RandomString(10);
                        p.S11 = RandomString(10);
                        p.S12 = RandomString(10);
                        p.S13 = RandomString(10);
                        p.S14 = RandomString(10);
                        p.S15 = RandomString(10);
                        p.S16 = RandomString(10);
                        p.S17 = RandomString(10);
                        p.S18 = RandomString(10);
                        p.S19 = RandomString(10);
                        p.S20 = RandomString(10);
                        p.S21 = RandomString(10);
                        p.S22 = RandomString(10);
                        p.S23 = RandomString(10);
                        p.S24 = RandomString(10);
                        p.S25 = RandomString(10);
                        p.S26 = RandomString(10);
                        p.S27 = RandomString(10);
                        p.S28 = RandomString(10);
                        p.S29 = RandomString(10);
                        p.S30 = RandomString(10);
                        p.S31 = RandomString(10);
                        p.S32 = RandomString(10);
                        p.S33 = RandomString(10);
                        p.S34 = RandomString(10);
                        p.S35 = RandomString(10);
                        p.S36 = RandomString(10);
                        p.S37 = RandomString(10);
                        p.S38 = RandomString(10);
                        p.S39 = RandomString(10);
                        p.S40 = RandomString(10);
                        p.S41 = RandomString(10);
                        p.S42 = RandomString(10);
                        p.S43 = RandomString(10);
                        p.S44 = RandomString(10);
                        p.S45 = RandomString(10);
                        p.S46 = RandomString(10);
                        p.S47 = RandomString(10);
                        p.S48 = RandomString(10);
                        p.S49 = RandomString(10);
                        p.S50 = RandomString(10);
                        p.N1 = createNamedEntity();
                        p.N2 = createNamedEntity();
                        p.N3 = createNamedEntity();
                        p.N4 = createNamedEntity();
                        p.N5 = createNamedEntity();
                        p.N6 = createNamedEntity();
                        p.N7 = createNamedEntity();
                        p.N8 = createNamedEntity();
                        p.N9 = createNamedEntity();
                        p.N10 = createNamedEntity();
                        p.N11 = createNamedEntity();
                        p.N12 = createNamedEntity();
                        p.N13 = createNamedEntity();
                        p.N14 = createNamedEntity();
                        p.N15 = createNamedEntity();
                        p.N16 = createNamedEntity();
                        p.N17 = createNamedEntity();
                        p.N18 = createNamedEntity();
                        p.N19 = createNamedEntity();
                        p.N20 = createNamedEntity();
                        p.N21 = createNamedEntity();
                        p.N22 = createNamedEntity();
                        p.N23 = createNamedEntity();
                        p.N24 = createNamedEntity();
                        p.N25 = createNamedEntity();
                        p.N26 = createNamedEntity();
                        p.N27 = createNamedEntity();
                        p.N28 = createNamedEntity();
                        p.N29 = createNamedEntity();
                        p.N30 = createNamedEntity();
                        p.N31 = createNamedEntity();
                        p.N32 = createNamedEntity();
                        p.N33 = createNamedEntity();
                        p.N34 = createNamedEntity();
                        p.N35 = createNamedEntity();
                        p.N36 = createNamedEntity();
                        p.N37 = createNamedEntity();
                        p.N38 = createNamedEntity();
                        p.N39 = createNamedEntity();
                        p.N40 = createNamedEntity();
                        p.N41 = createNamedEntity();
                        p.N42 = createNamedEntity();
                        p.N43 = createNamedEntity();
                        p.N44 = createNamedEntity();
                        p.N45 = createNamedEntity();
                        p.N46 = createNamedEntity();
                        p.N47 = createNamedEntity();
                        p.N48 = createNamedEntity();
                        p.N49 = createNamedEntity();
                        p.N50 = createNamedEntity();
                        return p;
                    });
            }
        }

        void TestProtobufImplicitAnnotatedEntities(int[] noOfItems)
        {
            Debug.WriteLine("-------------------------------------");
            Debug.WriteLine("TestProtobufImplicitAnnotatedEntities");
            Debug.WriteLine("-------------------------------------");
            //RuntimeTypeModel.Default.InferTagFromNameDefault = true; // not required

            Func<ProtobufImplicitAnnotatedEntities.NamedEntity> createNamedEntity = () => new ProtobufImplicitAnnotatedEntities.NamedEntity { Id = RandomString(5), Name = RandomString(10) };
            Func<ProtobufImplicitAnnotatedEntities.EntityIdentifier> createIdentifier = () => new ProtobufImplicitAnnotatedEntities.EntityIdentifier { Id = RandomString(5) };

            for (int i = 0; i < noOfItems.Length; i++)
            {
                Test(noOfItems[i], (m, l) => Serializer.Serialize(m, l), (m) => Serializer.Deserialize<List<ProtobufImplicitAnnotatedEntities.PersonEntity>>(m),
                    () =>
                    {
                        var p = new ProtobufImplicitAnnotatedEntities.PersonEntity();
                        p.Id = RandomString(5);
                        p.Gender = Gender.Male;
                        p.FirstName = RandomString(10);
                        p.LastName = RandomString(10);
                        p.Age = 10;
                        p.E1 = createIdentifier();
                        p.E2 = createIdentifier();
                        p.E3 = createIdentifier();
                        p.E4 = createIdentifier();
                        p.E5 = createIdentifier();
                        p.E6 = createIdentifier();
                        p.E7 = createIdentifier();
                        p.E8 = createIdentifier();
                        p.E9 = createIdentifier();
                        p.E10 = createIdentifier();
                        p.E11 = createIdentifier();
                        p.E12 = createIdentifier();
                        p.E13 = createIdentifier();
                        p.E14 = createIdentifier();
                        p.E15 = createIdentifier();
                        p.E16 = createIdentifier();
                        p.E17 = createIdentifier();
                        p.E18 = createIdentifier();
                        p.E19 = createIdentifier();
                        p.E20 = createIdentifier();
                        p.E21 = createIdentifier();
                        p.E22 = createIdentifier();
                        p.E23 = createIdentifier();
                        p.E24 = createIdentifier();
                        p.E25 = createIdentifier();
                        p.E26 = createIdentifier();
                        p.E27 = createIdentifier();
                        p.E28 = createIdentifier();
                        p.E29 = createIdentifier();
                        p.E30 = createIdentifier();
                        p.E31 = createIdentifier();
                        p.E32 = createIdentifier();
                        p.E33 = createIdentifier();
                        p.E34 = createIdentifier();
                        p.E35 = createIdentifier();
                        p.E36 = createIdentifier();
                        p.E37 = createIdentifier();
                        p.E38 = createIdentifier();
                        p.E39 = createIdentifier();
                        p.E40 = createIdentifier();
                        p.E41 = createIdentifier();
                        p.E42 = createIdentifier();
                        p.E43 = createIdentifier();
                        p.E44 = createIdentifier();
                        p.E45 = createIdentifier();
                        p.E46 = createIdentifier();
                        p.E47 = createIdentifier();
                        p.E48 = createIdentifier();
                        p.E49 = createIdentifier();
                        p.E50 = createIdentifier();
                        p.S1 = RandomString(10);
                        p.S2 = RandomString(10);
                        p.S3 = RandomString(10);
                        p.S4 = RandomString(10);
                        p.S5 = RandomString(10);
                        p.S6 = RandomString(10);
                        p.S7 = RandomString(10);
                        p.S8 = RandomString(10);
                        p.S9 = RandomString(10);
                        p.S10 = RandomString(10);
                        p.S11 = RandomString(10);
                        p.S12 = RandomString(10);
                        p.S13 = RandomString(10);
                        p.S14 = RandomString(10);
                        p.S15 = RandomString(10);
                        p.S16 = RandomString(10);
                        p.S17 = RandomString(10);
                        p.S18 = RandomString(10);
                        p.S19 = RandomString(10);
                        p.S20 = RandomString(10);
                        p.S21 = RandomString(10);
                        p.S22 = RandomString(10);
                        p.S23 = RandomString(10);
                        p.S24 = RandomString(10);
                        p.S25 = RandomString(10);
                        p.S26 = RandomString(10);
                        p.S27 = RandomString(10);
                        p.S28 = RandomString(10);
                        p.S29 = RandomString(10);
                        p.S30 = RandomString(10);
                        p.S31 = RandomString(10);
                        p.S32 = RandomString(10);
                        p.S33 = RandomString(10);
                        p.S34 = RandomString(10);
                        p.S35 = RandomString(10);
                        p.S36 = RandomString(10);
                        p.S37 = RandomString(10);
                        p.S38 = RandomString(10);
                        p.S39 = RandomString(10);
                        p.S40 = RandomString(10);
                        p.S41 = RandomString(10);
                        p.S42 = RandomString(10);
                        p.S43 = RandomString(10);
                        p.S44 = RandomString(10);
                        p.S45 = RandomString(10);
                        p.S46 = RandomString(10);
                        p.S47 = RandomString(10);
                        p.S48 = RandomString(10);
                        p.S49 = RandomString(10);
                        p.S50 = RandomString(10);
                        p.N1 = createNamedEntity();
                        p.N2 = createNamedEntity();
                        p.N3 = createNamedEntity();
                        p.N4 = createNamedEntity();
                        p.N5 = createNamedEntity();
                        p.N6 = createNamedEntity();
                        p.N7 = createNamedEntity();
                        p.N8 = createNamedEntity();
                        p.N9 = createNamedEntity();
                        p.N10 = createNamedEntity();
                        p.N11 = createNamedEntity();
                        p.N12 = createNamedEntity();
                        p.N13 = createNamedEntity();
                        p.N14 = createNamedEntity();
                        p.N15 = createNamedEntity();
                        p.N16 = createNamedEntity();
                        p.N17 = createNamedEntity();
                        p.N18 = createNamedEntity();
                        p.N19 = createNamedEntity();
                        p.N20 = createNamedEntity();
                        p.N21 = createNamedEntity();
                        p.N22 = createNamedEntity();
                        p.N23 = createNamedEntity();
                        p.N24 = createNamedEntity();
                        p.N25 = createNamedEntity();
                        p.N26 = createNamedEntity();
                        p.N27 = createNamedEntity();
                        p.N28 = createNamedEntity();
                        p.N29 = createNamedEntity();
                        p.N30 = createNamedEntity();
                        p.N31 = createNamedEntity();
                        p.N32 = createNamedEntity();
                        p.N33 = createNamedEntity();
                        p.N34 = createNamedEntity();
                        p.N35 = createNamedEntity();
                        p.N36 = createNamedEntity();
                        p.N37 = createNamedEntity();
                        p.N38 = createNamedEntity();
                        p.N39 = createNamedEntity();
                        p.N40 = createNamedEntity();
                        p.N41 = createNamedEntity();
                        p.N42 = createNamedEntity();
                        p.N43 = createNamedEntity();
                        p.N44 = createNamedEntity();
                        p.N45 = createNamedEntity();
                        p.N46 = createNamedEntity();
                        p.N47 = createNamedEntity();
                        p.N48 = createNamedEntity();
                        p.N49 = createNamedEntity();
                        p.N50 = createNamedEntity();
                        return p;
                    });
            }
        }

        void TestProtobufRuntimeRegistration(int[] noOfItems)
        {
            Debug.WriteLine("-------------------------------");
            Debug.WriteLine("TestProtobufRuntimeRegistration");
            Debug.WriteLine("-------------------------------");
            InitializeProtobufRunTime();

            Func<PlainEntities.NamedEntity> createNamedEntity = () => new PlainEntities.NamedEntity { Id = RandomString(5), Name = RandomString(10) };
            Func<PlainEntities.EntityIdentifier> createIdentifier = () => new PlainEntities.EntityIdentifier { Id = RandomString(5) };

            for (int i = 0; i < noOfItems.Length; i++)
            {
                Test(noOfItems[i], (m, l) => Serializer.Serialize(m, l), (m) => Serializer.Deserialize<List<PlainEntities.PersonEntity>>(m),
                    () =>
                    {
                        var p = new PlainEntities.PersonEntity();
                        p.Id = RandomString(5);
                        p.Gender = Gender.Male;
                        p.FirstName = RandomString(10);
                        p.LastName = RandomString(10);
                        p.Age = 10;
                        p.E1 = createIdentifier();
                        p.E2 = createIdentifier();
                        p.E3 = createIdentifier();
                        p.E4 = createIdentifier();
                        p.E5 = createIdentifier();
                        p.E6 = createIdentifier();
                        p.E7 = createIdentifier();
                        p.E8 = createIdentifier();
                        p.E9 = createIdentifier();
                        p.E10 = createIdentifier();
                        p.E11 = createIdentifier();
                        p.E12 = createIdentifier();
                        p.E13 = createIdentifier();
                        p.E14 = createIdentifier();
                        p.E15 = createIdentifier();
                        p.E16 = createIdentifier();
                        p.E17 = createIdentifier();
                        p.E18 = createIdentifier();
                        p.E19 = createIdentifier();
                        p.E20 = createIdentifier();
                        p.E21 = createIdentifier();
                        p.E22 = createIdentifier();
                        p.E23 = createIdentifier();
                        p.E24 = createIdentifier();
                        p.E25 = createIdentifier();
                        p.E26 = createIdentifier();
                        p.E27 = createIdentifier();
                        p.E28 = createIdentifier();
                        p.E29 = createIdentifier();
                        p.E30 = createIdentifier();
                        p.E31 = createIdentifier();
                        p.E32 = createIdentifier();
                        p.E33 = createIdentifier();
                        p.E34 = createIdentifier();
                        p.E35 = createIdentifier();
                        p.E36 = createIdentifier();
                        p.E37 = createIdentifier();
                        p.E38 = createIdentifier();
                        p.E39 = createIdentifier();
                        p.E40 = createIdentifier();
                        p.E41 = createIdentifier();
                        p.E42 = createIdentifier();
                        p.E43 = createIdentifier();
                        p.E44 = createIdentifier();
                        p.E45 = createIdentifier();
                        p.E46 = createIdentifier();
                        p.E47 = createIdentifier();
                        p.E48 = createIdentifier();
                        p.E49 = createIdentifier();
                        p.E50 = createIdentifier();
                        p.S1 = RandomString(10);
                        p.S2 = RandomString(10);
                        p.S3 = RandomString(10);
                        p.S4 = RandomString(10);
                        p.S5 = RandomString(10);
                        p.S6 = RandomString(10);
                        p.S7 = RandomString(10);
                        p.S8 = RandomString(10);
                        p.S9 = RandomString(10);
                        p.S10 = RandomString(10);
                        p.S11 = RandomString(10);
                        p.S12 = RandomString(10);
                        p.S13 = RandomString(10);
                        p.S14 = RandomString(10);
                        p.S15 = RandomString(10);
                        p.S16 = RandomString(10);
                        p.S17 = RandomString(10);
                        p.S18 = RandomString(10);
                        p.S19 = RandomString(10);
                        p.S20 = RandomString(10);
                        p.S21 = RandomString(10);
                        p.S22 = RandomString(10);
                        p.S23 = RandomString(10);
                        p.S24 = RandomString(10);
                        p.S25 = RandomString(10);
                        p.S26 = RandomString(10);
                        p.S27 = RandomString(10);
                        p.S28 = RandomString(10);
                        p.S29 = RandomString(10);
                        p.S30 = RandomString(10);
                        p.S31 = RandomString(10);
                        p.S32 = RandomString(10);
                        p.S33 = RandomString(10);
                        p.S34 = RandomString(10);
                        p.S35 = RandomString(10);
                        p.S36 = RandomString(10);
                        p.S37 = RandomString(10);
                        p.S38 = RandomString(10);
                        p.S39 = RandomString(10);
                        p.S40 = RandomString(10);
                        p.S41 = RandomString(10);
                        p.S42 = RandomString(10);
                        p.S43 = RandomString(10);
                        p.S44 = RandomString(10);
                        p.S45 = RandomString(10);
                        p.S46 = RandomString(10);
                        p.S47 = RandomString(10);
                        p.S48 = RandomString(10);
                        p.S49 = RandomString(10);
                        p.S50 = RandomString(10);
                        p.N1 = createNamedEntity();
                        p.N2 = createNamedEntity();
                        p.N3 = createNamedEntity();
                        p.N4 = createNamedEntity();
                        p.N5 = createNamedEntity();
                        p.N6 = createNamedEntity();
                        p.N7 = createNamedEntity();
                        p.N8 = createNamedEntity();
                        p.N9 = createNamedEntity();
                        p.N10 = createNamedEntity();
                        p.N11 = createNamedEntity();
                        p.N12 = createNamedEntity();
                        p.N13 = createNamedEntity();
                        p.N14 = createNamedEntity();
                        p.N15 = createNamedEntity();
                        p.N16 = createNamedEntity();
                        p.N17 = createNamedEntity();
                        p.N18 = createNamedEntity();
                        p.N19 = createNamedEntity();
                        p.N20 = createNamedEntity();
                        p.N21 = createNamedEntity();
                        p.N22 = createNamedEntity();
                        p.N23 = createNamedEntity();
                        p.N24 = createNamedEntity();
                        p.N25 = createNamedEntity();
                        p.N26 = createNamedEntity();
                        p.N27 = createNamedEntity();
                        p.N28 = createNamedEntity();
                        p.N29 = createNamedEntity();
                        p.N30 = createNamedEntity();
                        p.N31 = createNamedEntity();
                        p.N32 = createNamedEntity();
                        p.N33 = createNamedEntity();
                        p.N34 = createNamedEntity();
                        p.N35 = createNamedEntity();
                        p.N36 = createNamedEntity();
                        p.N37 = createNamedEntity();
                        p.N38 = createNamedEntity();
                        p.N39 = createNamedEntity();
                        p.N40 = createNamedEntity();
                        p.N41 = createNamedEntity();
                        p.N42 = createNamedEntity();
                        p.N43 = createNamedEntity();
                        p.N44 = createNamedEntity();
                        p.N45 = createNamedEntity();
                        p.N46 = createNamedEntity();
                        p.N47 = createNamedEntity();
                        p.N48 = createNamedEntity();
                        p.N49 = createNamedEntity();
                        p.N50 = createNamedEntity();
                        return p;
                    });
            }
        }

        void TestProtobufRuntimeRegistrationWithCompiledTypeModel(int[] noOfItems)
        {
            Debug.WriteLine("-------------------------------");
            Debug.WriteLine("TestProtobufRuntimeRegistrationWithCompiledTypeModel");
            Debug.WriteLine("-------------------------------");
            InitializeProtobufRunTimeWithCompiledTypeModel();


            Func<PlainEntities.NamedEntity> createNamedEntity = () => new PlainEntities.NamedEntity { Id = RandomString(5), Name = RandomString(10) };
            Func<PlainEntities.EntityIdentifier> createIdentifier = () => new PlainEntities.EntityIdentifier { Id = RandomString(5) };


            var t = typeof(PlainEntities.PersonEntity);
            var tList = typeof(IList<PlainEntities.PersonEntity>);
            var serializer = _compiledSerializer;
            for (int i = 0; i < noOfItems.Length; i++)
            {

                Test(noOfItems[i]
                    //, (m, l) => serializer.Serialize(m, l)
                    //, (m) => serializer.DeserializeItems<PlainEntities.PersonEntity>(m, PrefixStyle.Base128,1000).ToList()
                    , (m, l) => serializer.SerializeWithLengthPrefix(m, l, tList, PrefixStyle.Base128, 1000)
                    , (m) => serializer.DeserializeWithLengthPrefix(m, null, tList, PrefixStyle.Base128, 1000) as IList<PlainEntities.PersonEntity>,
                    () =>
                    {
                        var p = new PlainEntities.PersonEntity();
                        p.Id = RandomString(5);
                        p.Gender = Gender.Male;
                        p.FirstName = RandomString(10);
                        p.LastName = RandomString(10);
                        p.Age = 10;
                        p.E1 = createIdentifier();
                        p.E2 = createIdentifier();
                        p.E3 = createIdentifier();
                        p.E4 = createIdentifier();
                        p.E5 = createIdentifier();
                        p.E6 = createIdentifier();
                        p.E7 = createIdentifier();
                        p.E8 = createIdentifier();
                        p.E9 = createIdentifier();
                        p.E10 = createIdentifier();
                        p.E11 = createIdentifier();
                        p.E12 = createIdentifier();
                        p.E13 = createIdentifier();
                        p.E14 = createIdentifier();
                        p.E15 = createIdentifier();
                        p.E16 = createIdentifier();
                        p.E17 = createIdentifier();
                        p.E18 = createIdentifier();
                        p.E19 = createIdentifier();
                        p.E20 = createIdentifier();
                        p.E21 = createIdentifier();
                        p.E22 = createIdentifier();
                        p.E23 = createIdentifier();
                        p.E24 = createIdentifier();
                        p.E25 = createIdentifier();
                        p.E26 = createIdentifier();
                        p.E27 = createIdentifier();
                        p.E28 = createIdentifier();
                        p.E29 = createIdentifier();
                        p.E30 = createIdentifier();
                        p.E31 = createIdentifier();
                        p.E32 = createIdentifier();
                        p.E33 = createIdentifier();
                        p.E34 = createIdentifier();
                        p.E35 = createIdentifier();
                        p.E36 = createIdentifier();
                        p.E37 = createIdentifier();
                        p.E38 = createIdentifier();
                        p.E39 = createIdentifier();
                        p.E40 = createIdentifier();
                        p.E41 = createIdentifier();
                        p.E42 = createIdentifier();
                        p.E43 = createIdentifier();
                        p.E44 = createIdentifier();
                        p.E45 = createIdentifier();
                        p.E46 = createIdentifier();
                        p.E47 = createIdentifier();
                        p.E48 = createIdentifier();
                        p.E49 = createIdentifier();
                        p.E50 = createIdentifier();
                        p.S1 = RandomString(10);
                        p.S2 = RandomString(10);
                        p.S3 = RandomString(10);
                        p.S4 = RandomString(10);
                        p.S5 = RandomString(10);
                        p.S6 = RandomString(10);
                        p.S7 = RandomString(10);
                        p.S8 = RandomString(10);
                        p.S9 = RandomString(10);
                        p.S10 = RandomString(10);
                        p.S11 = RandomString(10);
                        p.S12 = RandomString(10);
                        p.S13 = RandomString(10);
                        p.S14 = RandomString(10);
                        p.S15 = RandomString(10);
                        p.S16 = RandomString(10);
                        p.S17 = RandomString(10);
                        p.S18 = RandomString(10);
                        p.S19 = RandomString(10);
                        p.S20 = RandomString(10);
                        p.S21 = RandomString(10);
                        p.S22 = RandomString(10);
                        p.S23 = RandomString(10);
                        p.S24 = RandomString(10);
                        p.S25 = RandomString(10);
                        p.S26 = RandomString(10);
                        p.S27 = RandomString(10);
                        p.S28 = RandomString(10);
                        p.S29 = RandomString(10);
                        p.S30 = RandomString(10);
                        p.S31 = RandomString(10);
                        p.S32 = RandomString(10);
                        p.S33 = RandomString(10);
                        p.S34 = RandomString(10);
                        p.S35 = RandomString(10);
                        p.S36 = RandomString(10);
                        p.S37 = RandomString(10);
                        p.S38 = RandomString(10);
                        p.S39 = RandomString(10);
                        p.S40 = RandomString(10);
                        p.S41 = RandomString(10);
                        p.S42 = RandomString(10);
                        p.S43 = RandomString(10);
                        p.S44 = RandomString(10);
                        p.S45 = RandomString(10);
                        p.S46 = RandomString(10);
                        p.S47 = RandomString(10);
                        p.S48 = RandomString(10);
                        p.S49 = RandomString(10);
                        p.S50 = RandomString(10);
                        p.N1 = createNamedEntity();
                        p.N2 = createNamedEntity();
                        p.N3 = createNamedEntity();
                        p.N4 = createNamedEntity();
                        p.N5 = createNamedEntity();
                        p.N6 = createNamedEntity();
                        p.N7 = createNamedEntity();
                        p.N8 = createNamedEntity();
                        p.N9 = createNamedEntity();
                        p.N10 = createNamedEntity();
                        p.N11 = createNamedEntity();
                        p.N12 = createNamedEntity();
                        p.N13 = createNamedEntity();
                        p.N14 = createNamedEntity();
                        p.N15 = createNamedEntity();
                        p.N16 = createNamedEntity();
                        p.N17 = createNamedEntity();
                        p.N18 = createNamedEntity();
                        p.N19 = createNamedEntity();
                        p.N20 = createNamedEntity();
                        p.N21 = createNamedEntity();
                        p.N22 = createNamedEntity();
                        p.N23 = createNamedEntity();
                        p.N24 = createNamedEntity();
                        p.N25 = createNamedEntity();
                        p.N26 = createNamedEntity();
                        p.N27 = createNamedEntity();
                        p.N28 = createNamedEntity();
                        p.N29 = createNamedEntity();
                        p.N30 = createNamedEntity();
                        p.N31 = createNamedEntity();
                        p.N32 = createNamedEntity();
                        p.N33 = createNamedEntity();
                        p.N34 = createNamedEntity();
                        p.N35 = createNamedEntity();
                        p.N36 = createNamedEntity();
                        p.N37 = createNamedEntity();
                        p.N38 = createNamedEntity();
                        p.N39 = createNamedEntity();
                        p.N40 = createNamedEntity();
                        p.N41 = createNamedEntity();
                        p.N42 = createNamedEntity();
                        p.N43 = createNamedEntity();
                        p.N44 = createNamedEntity();
                        p.N45 = createNamedEntity();
                        p.N46 = createNamedEntity();
                        p.N47 = createNamedEntity();
                        p.N48 = createNamedEntity();
                        p.N49 = createNamedEntity();
                        p.N50 = createNamedEntity();
                        return p;
                    });
            }
        }

        void TestProtobufPreCompiledTypeModel(int[] noOfItems)
        {
            Debug.WriteLine("-------------------------------");
            Debug.WriteLine("TestProtobufPreCompiledTypeModel");
            Debug.WriteLine("-------------------------------");

            Func<ProtobufFullyAnnotatedEntities.NamedEntity> createNamedEntity = () => new ProtobufFullyAnnotatedEntities.NamedEntity { Id = RandomString(5), Name = RandomString(10) };
            Func<ProtobufFullyAnnotatedEntities.EntityIdentifier> createIdentifier = () => new ProtobufFullyAnnotatedEntities.EntityIdentifier { Id = RandomString(5) };

            var serializer = new MySerializer();
            var tList = typeof(IList<ProtobufFullyAnnotatedEntities.PersonEntity>);

            for (int i = 0; i < noOfItems.Length; i++)
            {
                Test(noOfItems[i]
                    //, (m, l) => serializer.Serialize(m, l)
                    //, (m) => serializer.DeserializeItems<PlainEntities.PersonEntity>(m, PrefixStyle.Base128,1000).ToList()
                    , (m, l) => serializer.SerializeWithLengthPrefix(m, l, tList, PrefixStyle.Base128, 1000)
                    , (m) => serializer.DeserializeWithLengthPrefix(m, null, tList, PrefixStyle.Base128, 1000) as IList<ProtobufFullyAnnotatedEntities.PersonEntity>,
                    () =>
                    {
                        var p = new ProtobufFullyAnnotatedEntities.PersonEntity();
                        p.Id = RandomString(5);
                        p.Gender = Gender.Male;
                        p.FirstName = RandomString(10);
                        p.LastName = RandomString(10);
                        p.Age = 10;
                        p.E1 = createIdentifier();
                        p.E2 = createIdentifier();
                        p.E3 = createIdentifier();
                        p.E4 = createIdentifier();
                        p.E5 = createIdentifier();
                        p.E6 = createIdentifier();
                        p.E7 = createIdentifier();
                        p.E8 = createIdentifier();
                        p.E9 = createIdentifier();
                        p.E10 = createIdentifier();
                        p.E11 = createIdentifier();
                        p.E12 = createIdentifier();
                        p.E13 = createIdentifier();
                        p.E14 = createIdentifier();
                        p.E15 = createIdentifier();
                        p.E16 = createIdentifier();
                        p.E17 = createIdentifier();
                        p.E18 = createIdentifier();
                        p.E19 = createIdentifier();
                        p.E20 = createIdentifier();
                        p.E21 = createIdentifier();
                        p.E22 = createIdentifier();
                        p.E23 = createIdentifier();
                        p.E24 = createIdentifier();
                        p.E25 = createIdentifier();
                        p.E26 = createIdentifier();
                        p.E27 = createIdentifier();
                        p.E28 = createIdentifier();
                        p.E29 = createIdentifier();
                        p.E30 = createIdentifier();
                        p.E31 = createIdentifier();
                        p.E32 = createIdentifier();
                        p.E33 = createIdentifier();
                        p.E34 = createIdentifier();
                        p.E35 = createIdentifier();
                        p.E36 = createIdentifier();
                        p.E37 = createIdentifier();
                        p.E38 = createIdentifier();
                        p.E39 = createIdentifier();
                        p.E40 = createIdentifier();
                        p.E41 = createIdentifier();
                        p.E42 = createIdentifier();
                        p.E43 = createIdentifier();
                        p.E44 = createIdentifier();
                        p.E45 = createIdentifier();
                        p.E46 = createIdentifier();
                        p.E47 = createIdentifier();
                        p.E48 = createIdentifier();
                        p.E49 = createIdentifier();
                        p.E50 = createIdentifier();
                        p.S1 = RandomString(10);
                        p.S2 = RandomString(10);
                        p.S3 = RandomString(10);
                        p.S4 = RandomString(10);
                        p.S5 = RandomString(10);
                        p.S6 = RandomString(10);
                        p.S7 = RandomString(10);
                        p.S8 = RandomString(10);
                        p.S9 = RandomString(10);
                        p.S10 = RandomString(10);
                        p.S11 = RandomString(10);
                        p.S12 = RandomString(10);
                        p.S13 = RandomString(10);
                        p.S14 = RandomString(10);
                        p.S15 = RandomString(10);
                        p.S16 = RandomString(10);
                        p.S17 = RandomString(10);
                        p.S18 = RandomString(10);
                        p.S19 = RandomString(10);
                        p.S20 = RandomString(10);
                        p.S21 = RandomString(10);
                        p.S22 = RandomString(10);
                        p.S23 = RandomString(10);
                        p.S24 = RandomString(10);
                        p.S25 = RandomString(10);
                        p.S26 = RandomString(10);
                        p.S27 = RandomString(10);
                        p.S28 = RandomString(10);
                        p.S29 = RandomString(10);
                        p.S30 = RandomString(10);
                        p.S31 = RandomString(10);
                        p.S32 = RandomString(10);
                        p.S33 = RandomString(10);
                        p.S34 = RandomString(10);
                        p.S35 = RandomString(10);
                        p.S36 = RandomString(10);
                        p.S37 = RandomString(10);
                        p.S38 = RandomString(10);
                        p.S39 = RandomString(10);
                        p.S40 = RandomString(10);
                        p.S41 = RandomString(10);
                        p.S42 = RandomString(10);
                        p.S43 = RandomString(10);
                        p.S44 = RandomString(10);
                        p.S45 = RandomString(10);
                        p.S46 = RandomString(10);
                        p.S47 = RandomString(10);
                        p.S48 = RandomString(10);
                        p.S49 = RandomString(10);
                        p.S50 = RandomString(10);
                        p.N1 = createNamedEntity();
                        p.N2 = createNamedEntity();
                        p.N3 = createNamedEntity();
                        p.N4 = createNamedEntity();
                        p.N5 = createNamedEntity();
                        p.N6 = createNamedEntity();
                        p.N7 = createNamedEntity();
                        p.N8 = createNamedEntity();
                        p.N9 = createNamedEntity();
                        p.N10 = createNamedEntity();
                        p.N11 = createNamedEntity();
                        p.N12 = createNamedEntity();
                        p.N13 = createNamedEntity();
                        p.N14 = createNamedEntity();
                        p.N15 = createNamedEntity();
                        p.N16 = createNamedEntity();
                        p.N17 = createNamedEntity();
                        p.N18 = createNamedEntity();
                        p.N19 = createNamedEntity();
                        p.N20 = createNamedEntity();
                        p.N21 = createNamedEntity();
                        p.N22 = createNamedEntity();
                        p.N23 = createNamedEntity();
                        p.N24 = createNamedEntity();
                        p.N25 = createNamedEntity();
                        p.N26 = createNamedEntity();
                        p.N27 = createNamedEntity();
                        p.N28 = createNamedEntity();
                        p.N29 = createNamedEntity();
                        p.N30 = createNamedEntity();
                        p.N31 = createNamedEntity();
                        p.N32 = createNamedEntity();
                        p.N33 = createNamedEntity();
                        p.N34 = createNamedEntity();
                        p.N35 = createNamedEntity();
                        p.N36 = createNamedEntity();
                        p.N37 = createNamedEntity();
                        p.N38 = createNamedEntity();
                        p.N39 = createNamedEntity();
                        p.N40 = createNamedEntity();
                        p.N41 = createNamedEntity();
                        p.N42 = createNamedEntity();
                        p.N43 = createNamedEntity();
                        p.N44 = createNamedEntity();
                        p.N45 = createNamedEntity();
                        p.N46 = createNamedEntity();
                        p.N47 = createNamedEntity();
                        p.N48 = createNamedEntity();
                        p.N49 = createNamedEntity();
                        p.N50 = createNamedEntity();
                        return p;
                    });
            }
        }

        static void InitializeProtobufRunTime()
        {
            var assembly = Assembly.GetAssembly(typeof(PlainEntities.PersonEntity));
            var types = assembly.GetTypes();
            foreach (var t in types.Where(x => !string.IsNullOrWhiteSpace(x.Namespace) && x.Namespace.Contains("PlainEntities")))
            {
                Debug.WriteLine("Processing {0}", t.FullName);
                var meta = RuntimeTypeModel.Default.Add(t, false);
                var index = 1;

                // find any derived class for the entity
                foreach (var d in types.Where(x => x.IsSubclassOf(t)))
                {
                    var i = index++;
                    //Debug.WriteLine("\tSubtype: {0} - #{1}", d.Name, i);
                    meta.AddSubType(i, d);
                }

                // then add the properties
                foreach (var p in t.GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.DeclaredOnly).Where(x => x.GetSetMethod() != null))
                {
                    var i = index++;
                    //Debug.WriteLine("\tProperty: {0} - #{1}", p.Name, i);
                    meta.AddField(i, p.Name);
                }
            }
        }

        //static IDictionary<Type, RuntimeTypeModel> _caches = new Dictionary<Type, RuntimeTypeModel>();
        static TypeModel _compiledSerializer;

        static void InitializeProtobufRunTimeWithCompiledTypeModel()
        {
            var runtimeTypeModel = TypeModel.Create();

            var assembly = Assembly.GetAssembly(typeof(PlainEntities.PersonEntity));
            var types = assembly.GetTypes();
            foreach (var t in types.Where(x => !string.IsNullOrWhiteSpace(x.Namespace) && x.Namespace.Contains("PlainEntities")))
            {
                Debug.WriteLine("Processing {0}", t.FullName);

                var meta = runtimeTypeModel.Add(t, false);
                var index = 1;

                // find any derived class for the entity
                foreach (var d in types.Where(x => x.IsSubclassOf(t)))
                {
                    var i = index++;
                    //Debug.WriteLine("\tSubtype: {0} - #{1}", d.Name, i);
                    meta.AddSubType(i, d);
                }

                // then add the properties
                foreach (var p in t.GetProperties(BindingFlags.Instance | BindingFlags.Public).Where(x => x.GetSetMethod() != null))
                {
                    var i = index++;
                    //Debug.WriteLine("\tProperty: {0} - #{1}", p.Name, i);
                    meta.AddField(i, p.Name);
                }
            }
            _compiledSerializer = runtimeTypeModel.Compile();
        }

    }
}
