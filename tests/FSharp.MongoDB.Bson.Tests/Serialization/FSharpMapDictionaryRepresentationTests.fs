namespace FSharp.MongoDB.Bson.Tests.Serialization

open System
open CSharpDataModels
open FsUnit
open MongoDB.Bson
open MongoDB.Bson.Serialization.Attributes
open MongoDB.Bson.Serialization.Options
open NUnit.Framework

module FSharpMapDictionaryRepresentation =

    [<CLIMutable>]
    type FsUserId =
        { [<BsonGuidRepresentation(GuidRepresentation.Standard)>]
          Value: Guid }

    [<CLIMutable>]
    type FsUserMapDocument =
        { [<BsonDictionaryOptions(DictionaryRepresentation.ArrayOfDocuments)>]
          Users: Map<FsUserId, string> }

    let private userGuid = Guid.Parse("550e8400-e29b-41d4-a716-446655440000")

    let private expectedDocument =
        BsonDocument(
            [ BsonElement(
                  "Users",
                  BsonArray(
                      [ BsonDocument(
                            [ BsonElement("k", BsonDocument("Value", BsonBinaryData(userGuid, GuidRepresentation.Standard)))
                              BsonElement("v", BsonString("John")) ]) ])) ])

    [<Test>]
    let ``serialize map with complex key as array of documents`` () =
        let value =
            { Users =
                Map [ { Value = userGuid }, "John" ] }

        let result = serialize value
        result |> should equal expectedDocument

    [<Test>]
    let ``deserialize map with complex key from array of documents`` () =
        let result = deserialize<FsUserMapDocument> expectedDocument

        result.Users
        |> should equal (Map [ { Value = userGuid }, "John" ])

    [<Test>]
    let ``CSharp and FSharp models serialize the same array of documents representation`` () =
        let csUsers =
            System.Collections.Generic.Dictionary<CSharpDataModels.UserId, string>(
                dict [ (CSharpDataModels.UserId(userGuid), "John") ])

        let csValue =
            new CSharpDataModels.UserMapDocument(Users = csUsers)

        let fsValue =
            { Users =
                Map [ { Value = userGuid }, "John" ] }

        let csDoc = serialize csValue
        let fsDoc = serialize fsValue

        csDoc |> should equal expectedDocument
        fsDoc |> should equal expectedDocument

    [<Test>]
    let ``cross deserialize map with complex key using array of documents`` () =
        let csUsers =
            System.Collections.Generic.Dictionary<CSharpDataModels.UserId, string>(
                dict [ (CSharpDataModels.UserId(userGuid), "John") ])

        let csDoc =
            serialize (new CSharpDataModels.UserMapDocument(Users = csUsers))

        let fsValue = deserialize<FsUserMapDocument> csDoc
        fsValue.Users |> should equal (Map [ { Value = userGuid }, "John" ])

        let fsDoc =
            serialize
                { Users =
                    Map [ { Value = userGuid }, "John" ] }

        let csValue = deserialize<CSharpDataModels.UserMapDocument> fsDoc
        csValue.Users.Count |> should equal 1
        csValue.Users[CSharpDataModels.UserId(userGuid)] |> should equal "John"
