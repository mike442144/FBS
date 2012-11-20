<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	SampleTags
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h1>&lt;H1&gt; Sample Tags</h1>

	<h2>&lt;H2&gt; Sample Messages</h2>
	
	<div class="errorMessage">Error Message</div>
    <div class="warningMessage">Warning Message</div>    
    <div class="infoMessage">Info Message</div>
    
	<h2>&lt;H2&gt; Sample Tags</h2>

	<h3>&lt;H3&gt; Code</h3>
    <p>
        <code>
            code-sample {
            <br />
            font-weight: bold;<br />
            font-style: italic;<br />
            }
        </code>
    </p>
    
    <h3>&lt;H3&gt; Example Lists</h3>
	<ol>
		<li>Here is an example</li>
		<li>of an ordered list</li>							
	</ol>			
	<ul>
		<li>Here is an example</li>
		<li>of an unordered list</li>							
	</ul>				
	
	<h3>&lt;H3&gt; Blockquote</h3>
    <blockquote>
        <p>
            Lorem ipsum dolor sit amet, consectetuer adipiscing elit, sed diam nonummy nibh
            euismod tincidunt ut laoreet dolore magna aliquam erat....
        </p>
    </blockquote>
    
    <h3>&lt;H3&gt; Image and text</h3>
    <p>
        <a href="#"><img src="../../Content/sample/ie.jpg" alt="Internet Expolorer" class="float-left" /></a>
        Lorem ipsum dolor sit amet, consectetuer adipiscing elit. Donec libero. Suspendisse
        bibendum. Cras id urna. Morbi tincidunt, orci ac convallis aliquam, lectus turpis
        varius lorem, eu posuere nunc justo tempus leo. Donec mattis, purus nec placerat
        bibendum, dui pede condimentum odio, ac blandit ante orci ut diam. Cras fringilla
        magna. Phasellus suscipit, leo a pharetra condimentum, lorem tellus eleifend magna,
        eget fringilla velit magna id neque. Curabitur vel urna. In tristique orci porttitor
        ipsum. Aliquam ornare diam iaculis nibh.
    </p>
    <p class="clear">
        <a href="#"><img src="../../Content/sample/ie.jpg" alt="Internet Expolorer" class="float-right" /></a>
        Lorem ipsum dolor sit amet, consectetuer adipiscing elit. Donec libero. Suspendisse
        bibendum. Cras id urna. Morbi tincidunt, orci ac convallis aliquam, lectus turpis
        varius lorem, eu posuere nunc justo tempus leo. Donec mattis, purus nec placerat
        bibendum, dui pede condimentum odio, ac blandit ante orci ut diam. Cras fringilla
        magna. Phasellus suscipit, leo a pharetra condimentum, lorem tellus eleifend magna,
        eget fringilla velit magna id neque. Curabitur vel urna. In tristique orci porttitor
        ipsum. Aliquam ornare diam iaculis nibh.
    </p>	
	<h3>&lt;H3&gt;Table Styling</h3>
				
	<table>
	    <thead>
	        <tr>
	            <td colspan="3">Table Description</td>
	        </tr>
	    </thead>
	    <tbody>
		    <tr>
			    <th><strong>post</strong> date</th>
			    <th>title</th>
			    <th>description</th>
		    </tr>
		    <tr>
			    <td>05.31.2006</td>
			    <td>Augue non nibh</td>
			    <td>Lobortis commodo metus vestibulum</td>
		    </tr>
		    <tr class="row-alternate">
			    <td>05.31.2006</td>
			    <td>Fusce ut diam bibendum</td>
			    <td>Purus in eget odio in sapien</td>
		    </tr>
		    <tr>
			    <td>05.31.2006</td>
			    <td>Maecenas et ipsum</td>
			    <td>Adipiscing blandit quisque eros</td>
		    </tr>
		    <tr class="row-alternate">
			    <td>05.31.2006</td>
			    <td>Sed vestibulum blandit</td>
			    <td>Cras lobortis commodo metus lorem</td>
		    </tr>
	    </tbody>
	</table>

	<h3>Example Form</h3>
	<form action="#">
        <fieldset>
            <legend>Account Information</legend>
            <p>
                <label>Name:</label>
                <input name="name" value="Your Name" type="text" size="30" />
                <label>Email:</label>
                <input name="email" value="Your Email" type="text" size="30" />
                <label>Comments:</label>
                <textarea rows="5" cols="5"></textarea>
                <br />
                <input class="button" type="submit" />
            </p>
        </fieldset>
    </form>	
</asp:Content>
