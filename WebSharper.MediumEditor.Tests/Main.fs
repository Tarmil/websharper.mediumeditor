// $begin{copyright}
//
// This file is part of WebSharper
//
// Copyright (c) 2008-2018 IntelliFactory
//
// Licensed under the Apache License, Version 2.0 (the "License"); you
// may not use this file except in compliance with the License.  You may
// obtain a copy of the License at
//
//     http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or
// implied.  See the License for the specific language governing
// permissions and limitations under the License.
//
// $end{copyright}
namespace WebSharper.MediumEditor.Tests

open WebSharper
open WebSharper.MediumEditor
open WebSharper.Sitelets
open WebSharper.UI
open WebSharper.UI.Client
open WebSharper.UI.Html

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
                let editor = Elt.div [] []
                notEqualMsg (MediumEditor(editor.Dom, MediumEditorOptions())) (JS.Undefined) "Medium Editor constructor"
            }
        }

    [<SPAEntryPoint>]
    let RunTests() =
        let e = Runner.RunTests [Tests]
        e.ReplaceInDom(JS.Document.QuerySelector "#body")
