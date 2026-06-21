namespace FSharp.MongoDB.Bson.Tests.Serialization

open System
open CSharpDataModels
open FsUnit
open MongoDB.Bson
open MongoDB.Bson.Serialization.Attributes
open MongoDB.Bson.Serialization.Options
open MongoDB.Bson.Serialization.Serializers
open NUnit.Framework

module IsomorphicSerialization =

    type GuidStringMapSerializer() =
        inherit
            FSharpMapSerializer<Guid, string>(
                DictionaryInterfaceImplementerSerializer<System.Collections.Generic.Dictionary<Guid, string>, Guid, string>(
                    DictionaryRepresentation.ArrayOfDocuments,
                    GuidSerializer(GuidRepresentation.Standard),
                    StringSerializer()))

    type Pair =
        { First: int
          Second: string option }

    [<RequireQualifiedAccess>]
    type Value =
        | IntValue of Value:int
        | StringValue of Value: string
        | PairValue of Value:Pair

    [<CLIMutable>]
    type RecordDataModel =
        { Id: ObjectId
            
          Int: int
          IntOpt: int option
          
          String: string
          StringOpt: string option
          
          Array: int array
          ArrayOpt: int array option

          Value: Value
          ValueOpt: Value option

          ValueArray: Value array
          ValueArrayOpt: Value array option

          Record: Pair
          RecordOpt: Pair option
          
          Map: Map<string, int>
          [<BsonDictionaryOptions(DictionaryRepresentation.ArrayOfDocuments)>]
          IntKeyMap: Map<int, int>
          [<BsonSerializer(typeof<GuidStringMapSerializer>)>]
          GuidKeyMap: Map<Guid, string> }

    let ModelSome() =
        let csModel = 
            let map =
                let map = System.Collections.Generic.Dictionary<string, int>()
                map.Add("1", 1)
                map.Add("2", 2)
                map

            let intKeyMap =
                let map = System.Collections.Generic.Dictionary<int, int>()
                map.Add(1, 10)
                map.Add(2, 20)
                map

            let guidKeyMap =
                let map = System.Collections.Generic.Dictionary<Guid, string>()
                map.Add(Guid.Parse("1c6d4e6a-8f8c-4a9d-a7ce-28a7f6f1d111"), "two")
                map.Add(Guid.Parse("550e8400-e29b-41d4-a716-446655440000"), "one")
                map

            CSharpDataModels.RecordDataModel(
                Id = ObjectId.GenerateNewId(),
                Int = 42,
                IntOpt = 666,
                String = "String",
                StringOpt = "StringOpt",
                Array = [| 1; 2; 3 |],
                ArrayOpt = [| 5; 6; 7; 8 |],
                Value = CSharpDataModels.Value.IntValue(42),
                ValueOpt = CSharpDataModels.Value.StringValue("ValueStringOpt"),
                ValueArray = [| CSharpDataModels.Value.IntValue(42)
                                CSharpDataModels.Value.StringValue("String")
                                CSharpDataModels.Value.PairValue(CSharpDataModels.Pair(First = 99, Second = "SecondPair")) |],
                ValueArrayOpt = [| CSharpDataModels.Value.IntValue(101) |],
                Record = CSharpDataModels.Pair(First = 1, Second = "Second"),
                RecordOpt = CSharpDataModels.Pair(First = -1, Second = "SecondOpt"),
                Map = map,
                IntKeyMap = intKeyMap,
                GuidKeyMap = guidKeyMap)

        let fsModel =
            { Id = csModel.Id
              Int = 42
              IntOpt = Some 666
              String = "String"
              StringOpt = Some "StringOpt"
              Array = [| 1; 2; 3 |]
              ArrayOpt = Some [| 5; 6; 7; 8 |]
              Value = Value.IntValue 42
              ValueOpt = Some <| Value.StringValue "ValueStringOpt"
              ValueArray = [| Value.IntValue 42; Value.StringValue "String"; Value.PairValue { First = 99; Second = Some "SecondPair" } |]
              ValueArrayOpt = Some [| Value.IntValue 101 |]
              Record = { First = 1; Second = Some "Second" }
              RecordOpt = Some { First = -1; Second = Some "SecondOpt" }
              Map = Map [ "1", 1; "2", 2 ]
              IntKeyMap = Map [ 1, 10; 2, 20 ]
              GuidKeyMap =
                Map [ Guid.Parse("550e8400-e29b-41d4-a716-446655440000"), "one"
                      Guid.Parse("1c6d4e6a-8f8c-4a9d-a7ce-28a7f6f1d111"), "two" ] }

        csModel, fsModel

    let ModelNone() =
        let csModel = 
            let map =
                let map = System.Collections.Generic.Dictionary<string, int>()
                map.Add("1", 1)
                map.Add("2", 2)
                map

            let intKeyMap =
                let map = System.Collections.Generic.Dictionary<int, int>()
                map.Add(1, 10)
                map.Add(2, 20)
                map

            let guidKeyMap =
                let map = System.Collections.Generic.Dictionary<Guid, string>()
                map.Add(Guid.Parse("1c6d4e6a-8f8c-4a9d-a7ce-28a7f6f1d111"), "two")
                map.Add(Guid.Parse("550e8400-e29b-41d4-a716-446655440000"), "one")
                map

            CSharpDataModels.RecordDataModel(
                Id = ObjectId.GenerateNewId(),
                Int = 42,
                String = "String",
                Array = [| 1; 2; 3 |],
                Value = CSharpDataModels.Value.IntValue(42),
                ValueArray = [| CSharpDataModels.Value.IntValue(42)
                                CSharpDataModels.Value.StringValue("String")
                                CSharpDataModels.Value.PairValue(CSharpDataModels.Pair(First = 99, Second = "SecondPair")) |],
                Record = CSharpDataModels.Pair(First = 1, Second = "Second"),
                Map = map,
                IntKeyMap = intKeyMap,
                GuidKeyMap = guidKeyMap)

        let fsModel =
            { Id = csModel.Id
              Int = 42
              IntOpt = None
              String = "String"
              StringOpt = None
              Array = [| 1; 2; 3 |]
              ArrayOpt = None
              Value = Value.IntValue 42
              ValueOpt = None
              ValueArray = [| Value.IntValue 42; Value.StringValue "String"; Value.PairValue { First = 99; Second = Some "SecondPair" } |]
              ValueArrayOpt = None
              Record = { First = 1; Second = Some "Second" }
              RecordOpt = None
              Map = Map [ "1", 1; "2", 2 ]
              IntKeyMap = Map [ 1, 10; 2, 20 ]
              GuidKeyMap =
                Map [ Guid.Parse("550e8400-e29b-41d4-a716-446655440000"), "one"
                      Guid.Parse("1c6d4e6a-8f8c-4a9d-a7ce-28a7f6f1d111"), "two" ] }

        csModel, fsModel

    [<Test>]
    let ``Isomorphic Bson Some cs / fs``() =
        let csModel, fsModel = ModelSome()

        // serialization shall be same
        let csDoc = serialize csModel
        let fsDoc = serialize fsModel
        csDoc |> should equal fsDoc

        // cross-deserialization shall work
        let fsModel2 = deserialize<RecordDataModel> csDoc
        fsModel2 |> should equal fsModel

    [<Test>]
    let ``Isomorphic Bson None cs / fs``() =
        let csModel, fsModel = ModelNone()

        let csDoc = serialize csModel
        let fsDoc = serialize fsModel
        csDoc |> should equal fsDoc

        // cross-deserialization shall work
        let fsModel2 = deserialize<RecordDataModel> csDoc
        fsModel2 |> should equal fsModel
