namespace Cyjs.NET

open System
open DynamicObj


module CyParam =

    type CyStyleParam = {
      Name        : string
      Value       : obj
    }
    

    type CyStyleClass = {
      Class       : string
    }


    let applyCyStyle (dyObj:#DynamicObj) (style:CyStyleParam) =
        style.Value |> DynObj.setValue dyObj style.Name
        dyObj

    let applyCyStyles (dyObj:#DynamicObj) (styles:seq<CyStyleParam>) =
        styles
        |> Seq.iter (fun style -> 
            style.Value |> DynObj.setValue dyObj style.Name
            )
        dyObj
        


    let inline style name v  = {
        Name = name
        Value = v
    }

    let inline cyClass name = { Class = name }

    let color v   = style "color" v
    let content v = style "content" v
    let height v  = style "height" v
    let label v   = style "label" v
    let opacity v = style "opacity" v 
    
    let shape v   = style "shape" v
    module Shape =
        let triangle  =  style "shape" "triangle"
        let ellipse   =  style "shape" "ellipse"
        let octagon   =  style "shape" "octagon"
        let rectangle =  style "shape" "rectangle"

    let weight v  = style "weight" v
    let width v   = style "width" v
    
    module Background =
        let color v   = style "background-color" v

    module Border =
        let color v   = style "border-color" v                
        let width v   = style "border-width" v

    module Curve =
        let style v   = style "curve-style" v

    module Line =
        let color v   = style "line-color" v
        let style v   = style "border-style" v

    module Source =
        module Arrow =
            let shape v = style "source-arrow-shape" v
            let color v = style "source-arrow-color" v

    module Target =     
        module Arrow =
            let shape v = style "target-arrow-shape" v
            let color v = style "target-arrow-color" v

    module Text =        
        module Align =
            let center = style "text-valign" "center"
            let left = style "text-valign" "left"
            let right = style "text-valign" "right"
        module Outline =
            let width v = style "text-outline-width" v
            let color v = style "text-outline-color" v
        let opacity v = style "text-opacity" v


