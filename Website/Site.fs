namespace Website

open IntelliFactory.Html
open IntelliFactory.WebSharper.Sitelets

type Pages =
    | [<CompiledName "/">] Home
    | D3

type Site() =
    interface IWebsite<Pages> with
        member __.Actions = []
        member __.Sitelet =
            Sitelet.Infer <| function
                | Home -> Content.Redirect D3
                | D3 ->
                    PageContent <| fun ctx ->
                        {
                            Page.Default with
                                Title = Some "D3 Examples"
                                Body = [ Div [ new D3Examples() ] ]
                        }

[<assembly: Website(typeof<Site>)>]
do ()