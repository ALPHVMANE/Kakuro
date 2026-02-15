<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Levels.aspx.cs" Inherits="Kakuro.Views.Levels" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>

    <style>
    	$chipSize : 60px;
    	$headingFont: "Fredoka One", Calibri, sans-serif;
    	@mixin roundIt() {
    		border-radius: 10px;
    	}

    	body {
    		background: #222;
    		font-family: Calibri, sans-serif;
    		color: white;
    		text-align: center;
    		max-width: 560px;
    		min-width: 320px;
    		margin: 2em auto;
    		padding: 3em;
    	}

    	.title {
    		font-family: $headingFont;
    		text-transform: uppercase;
    		font-weight: normal;
    		color: white;
    		font-size: 2em;
    		margin: 1em;
    	}

    	#levels-screen {
    		min-width: 320px;
    		width: 60%;
    		margin: 10vh auto;
    		h1

    	{
    		text-align: center;
    	}

    	li {
    		@include roundIt();
    		width: $chipSize;
    		height: $chipSize;
    		font-family: $headingFont;
    		float: left;
    		margin: 10px;
    		line-height: $chipSize;
    		text-align: center;
    		font-weight: bold;
    		font-size: 1.3em;
    		text-shadow: 0 2px 0 rgba(black, .3);
    		&.locked-level

    	{
    		background-color: gray;
    	}

    	}
    	}

    	.easy {
    		background: lightgreen;
    	}

    	.medium {
    		background: lemonchiffon;
    	}

    	.hard {
    		background: lightsalmon;
    	}

    	.insane {
    		background: salmon;
    	}
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div>
        </div>
    </form>
</body>
</html>
