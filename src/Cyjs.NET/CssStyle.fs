namespace Cyjs.NET

open System
open DynamicObj

type CssStyle = {
  Name        : string
  Value       : obj
}

type CssClass = {
  Class       : string
}

type CssRule = {
  Selector    : string
  Styles      : CssStyle ResizeArray
  Rules       : CssRule ResizeArray
}
  with
    member self.Add (x : obj) =
      match x with
      | :? CssStyle as x -> self.Styles.Add x
      | :? CssRule as x -> self.Rules.Add x
      | :? Collections.IEnumerable as x -> for y in x do self.Add y
      | _ -> ()