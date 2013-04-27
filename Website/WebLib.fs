[<AutoOpen>]
module Website.WebLib

open IntelliFactory.WebSharper
open IntelliFactory.WebSharper.Pervasives

type JS = JavaScriptAttribute

[<Inline "Math.random()">]
let random() : float = X