namespace Cyjs.NET

open Newtonsoft.Json

type PlainJsonString(str:string) = 
    member val Value = str with get,set

type PlainJsonStringConverter() =
    inherit JsonConverter()

    override __.CanConvert(objectType) =
        typeof<PlainJsonString> = objectType
    
    override __.ReadJson(reader, objectType, existingValue, serializer) =
        reader.Value

    override __.WriteJson(writer, value, serializer) =
        let v = value :?> PlainJsonString
        writer.WriteRawValue(string v.Value)