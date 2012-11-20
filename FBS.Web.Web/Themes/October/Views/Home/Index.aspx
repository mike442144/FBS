<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="indexContent" ContentPlaceHolderID="MainContent" runat="server">
    <h2><%= Html.Encode(ViewData["Message"]) %> (h2)</h2>
    <p>
        This is a standard paragraph with an external link to the <a href="http://asp.net/mvc">ASP.NET MVC website</a>.
    </p>
    <p>
        Lorem ipsum dolor sit amet, consectetur adipiscing elit. Aenean accumsan, ipsum non malesuada sagittis, nibh odio sollicitudin est, eget sagittis turpis lorem ut nulla.
    </p>
    <p>
        Morbi a erat. Quisque porta quam eu dui bibendum euismod. Curabitur scelerisque quam in massa. Curabitur aliquet laoreet arcu. Nulla mi. Vestibulum ante ipsum primis in faucibus orci luctus et ultrices posuere cubilia Curae; Morbi scelerisque dui in turpis.
    </p>
    <h3>Lists (h3)</h3>
    <ul>
        <li>This is a link to a <a href="image.jpg">JPG image</a></li>
        <li>This is a link to a <a href="image.png">PNG image</a></li>
        <li>This is a link to a <a href="image.gif">GIF image</a></li>
    </ul>
    <ol>
        <li>This is a link to a <a href="document.txt">text file</a></li>
        <li>HTML files:
            <ol>
                <li>This is a link to a <a href="document.htm">HTML file</a> (extension .htm)</li>
                <li>This is a link to a <a href="document.html">HTML file</a> (extension .html)</li>
            </ol>
        </li>
        <li>Microsoft Office documents:
            <ol>
                <li>This is a link to a <a href="document.doc">Word 2003 document</a></li>
                <li>This is a link to a <a href="document.docx">Word 2007 document</a></li>
            </ol>
        </li>
        <li>This is a link to a <a href="document.pdf">PDF file</a></li>
    </ol>
    <h4>Tables (h4)</h4>
    <table id="table">
        <thead>
            <tr>
                <th></th>
                <th>A</th>
                <th>B</th>
                <th>C</th>
            </tr>
        </thead>
        <tbody>
            <tr>
                <th>1</th>
                <td>Cell A1</td>
                <td>Cell B1</td>
                <td>Cell C1</td>
            </tr>
            <tr>
                <th>2</th>
                <td>Cell A2</td>
                <td>Cell B2</td>
                <td>Cell C2</td>
            </tr>
            <tr>
                <th>3</th>
                <td>Cell A3</td>
                <td>Cell B3</td>
                <td>Cell C3</td>
            </tr>
            <tr>
                <th>4</th>
                <td>Cell A4</td>
                <td>Cell B4</td>
                <td>Cell C4</td>
            </tr>
        </tbody>
    </table>
    <h5>Form elements (h5)</h5>
    <form action="#" method="post">
        <fieldset>
            <legend>Text input</legend>
            <label for="inputtext">Label for text input:</label><input id="inputtext" name="inputtext" type="text" value="Lorem ipsum" />
            <label for="textarea">Label for textarea:</label><textarea id="textarea" name="textarea">Lorem ipsum dolor sit amet, consectetur adipiscing elit. Vestibulum molestie tempor nulla. Nam quis orci. Morbi justo dolor, vehicula a, lacinia non, molestie eget, enim. Pellentesque aliquet erat eget erat. Cum sociis natoque penatibus et magnis dis parturient montes, nascetur ridiculus mus. Suspendisse potenti. Nunc semper nulla sit amet risus. Nullam bibendum consectetur quam. Vivamus eget dui. Mauris lacus. Duis ac quam interdum ligula tincidunt posuere. Duis aliquet dui et felis. Donec posuere augue. Ut euismod magna nec justo. Pellentesque ut mauris nec ipsum dapibus dignissim. Maecenas eget nunc sed elit placerat iaculis.</textarea>
            <label for="password">Label for password input:</label><input type="password" id="password" value="secret" />
        </fieldset>
        <fieldset>
            <legend>Selection</legend>
            <p>
                <label for="select">Label for select:</label>
                <select id="select" name="select">
                    <optgroup id="optgroup1" label="Option group 1">
                        <option value="option1">Option 1</option>
                        <option value="option2">Option 2</option>
                        <option value="option3">Option 3</option>
                    </optgroup>
                    <optgroup id="optgroup2" label="Option group 2">
                        <option value="option4">Option 4</option>
                        <option value="option5">Option 5</option>
                        <option value="option6">Option 6</option>
                    </optgroup>
                </select>
            </p>
            <p>
                <input type="checkbox" id="checkbox1" name="checkbox1" value="checkbox1" checked="checked" /><label for="checkbox1">Label for checkbox</label>
                <input type="checkbox" id="checkbox2" name="checkbox2" /><label for="checkbox2">Label for checkbox</label>
                <input type="checkbox" id="checkbox3" name="checkbox3" /><label for="checkbox3">Label for checkbox</label>
            </p>
            <p>
                <input type="radio" id="radiobutton1"  name="radiobutton" value="radiobutton1" checked="checked" /><label for="radiobutton1">Label for radiobutton</label>
                <input type="radio" id="radiobutton2"  name="radiobutton" value="radiobutton2" /><label for="radiobutton2">Label for radiobutton</label>
            </p>
        </fieldset>
        <fieldset>
            <legend>Buttons</legend>
            <button id="realbutton" value="realbutton">This is a real button (&lt;button&gt;)</button>
            <input type="button" id="inputbutton" value="this is a input button (&lt;input type=&quot;button&quot;&gt;)" />
        </fieldset>
    </form>
    <h6>Messages (h6)</h6>
    <div class="information">
        This is a simple information box.
    </div>
    <div class="error">
        This is a simple error box.
    </div>
    <div class="success">
        This is a simple success box.
    </div>
</asp:Content>
