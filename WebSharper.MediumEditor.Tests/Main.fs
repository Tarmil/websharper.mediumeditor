namespace WebSharper.MediumEditor.Tests

open WebSharper
open WebSharper.MediumEditor
open WebSharper.Sitelets
open WebSharper.UI.Next
open WebSharper.UI.Next.Client
open WebSharper.UI.Next.Html

[<JavaScript>]
module Client =
    open WebSharper.JavaScript
    open WebSharper.Testing

    let Tests =
        TestCategory "General" {

            Test "Sanity check" {
                equalMsg (1 + 1) 2 "1 + 1 = 2"
            }

            Test "Constructor test" {
                let editor = div []
                notEqualMsg (MediumEditor(editor.Dom, MediumEditorOptions())) (JS.Undefined) "Medium Editor constructor"
            }
        }

#if ZAFIR
    let RunTests() =
        Runner.RunTests [
            Tests
        ]
#endif

module Site =
    open WebSharper.UI.Next.Server
    open WebSharper.UI.Next.Html

    [<Website>]
    let Main =
        Application.SinglePage (fun ctx ->
            Content.Page(
                Title = "WebSharper.MediumEditor Tests",
                Body = [
#if ZAFIR
                    client <@ Client.RunTests() @>
#else
                    WebSharper.Testing.Runner.Run [
                        System.Reflection.Assembly.GetExecutingAssembly()
                    ]
                    |> Doc.WebControl
#endif
                ]
            )
        )
