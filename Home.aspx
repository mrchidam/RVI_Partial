<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Home.aspx.cs" Inherits="Home" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>Grahams Door Tracking</title>
<link rel="stylesheet" type="text/css" href="css/style.css">
<link rel="stylesheet" type="text/css" href="css/doorgroup_master12.css">
<link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/font-awesome/4.4.0/css/font-awesome.min.css">

   <script src="http://code.jquery.com/jquery-1.9.1.js"></script>
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/1.11.3/jquery.min.js"></script>
<link href="http://ajax.googleapis.com/ajax/libs/jqueryui/1.8/themes/base/jquery-ui.css" rel="stylesheet" type="text/css"/>  
<script src="http://ajax.googleapis.com/ajax/libs/jquery/1.5/jquery.min.js"></script>  
<script src="http://ajax.googleapis.com/ajax/libs/jqueryui/1.8/jquery-ui.min.js"></script>
<script src="Scripts/doorgroup_script.js" type="text/javascript"></script>
</head>
<body>
    <form id="Form1" runat="server">

        
  <header>
    <div class="logo"> 
	
		  <div class="logo_img"> 
			   <img src="images/logo.png"/> 
		  </div>
		  
		  <div class="logo_img_right"> 
			   <img src="images/logo_right.jpg"/> 
		  </div>
		  
	</div>
  </header>
   <nav>
  
	<a href="Home.aspx">Home</a> 
	
	<a href="Settings.aspx" style="background-color: #0096c9; color: #ffffff; border: 1px solid #0096c9; border-bottom:0;">Tools</a> 
	
  </nav> 

    <section class="middle_section">
  
 
     <div class="order_box_top_section">
  
   		<div class="selection_box">
		       <label>System:</label>
		       <select id="list" runat="server">
				  
				  <option value="0">Order Document</option>
                   <option value="1">Drawings</option>
				</select>
		 </div>
			
		<div class="date_calendar-col"> <label>Date</label> <input type="date" class="datepicker imgcal" name="PromiseDate" value="" id="txtdate" runat="server"/> </div>  
     
	 </div>
	 
     <div class="order_box">
 
		<div class="order_box_p1 drawings" style="width:37%; height:246px;">
			<br />
			<div class="input-col"> <label>Draw No:</label> <asp:TextBox runat="server" ID="txtdrawno"></asp:TextBox> </div>
			
			<div class="input-col"> <label>Revision:</label> <asp:TextBox runat="server" ID="txtrevision"></asp:TextBox> </div>
			
			<div class="input-col"> <label>Run No:</label> <asp:TextBox runat="server" ID="txtrunno"></asp:TextBox> </div>
			
			<div class="input-col"> <label>Call No:</label> <asp:TextBox runat="server" ID="txtcallno"></asp:TextBox> </div>
			
			 <div class="selection_box">
				   <label>Product:</label>
				  <asp:DropDownList runat="server" ID="ddlproduct">
                      <asp:ListItem Value="D">D</asp:ListItem>
                      <asp:ListItem Value="F">F</asp:ListItem>
				  </asp:DropDownList>
			</div>
			
			 <div class="selection_box">
				   <label>Document Type:</label>
				   <asp:DropDownList runat="server" ID="ddldocumenttypedrawing">
                       <asp:ListItem Value="RUN PROD DWG">RUN PROD DWG</asp:ListItem>
                       <asp:ListItem Value="CUR DRAWNG">CUR DRAWNG</asp:ListItem>
                       <asp:ListItem Value="GRA DRAWNG">GRA DRAWNG</asp:ListItem>
				   </asp:DropDownList>
			</div>			
		
		</div>
		
		<div class="order_box_p1 orderdocument" style="display:none; width:37%;">
		
			 <div class="selection_box">
				   <label>Company:</label>
				   <asp:DropDownList runat="server" ID="ddlcompany">
                       <asp:ListItem Value="0">CURR - Curries</asp:ListItem>
                       <asp:ListItem Value="1">GRAH - Graham</asp:ListItem>
                       <asp:ListItem Value="2">CECO - Ceco</asp:ListItem>
                       <asp:ListItem Value="3">FRMW - Frameworks</asp:ListItem>
                      
				   </asp:DropDownList>
			</div>
		
			<div class="input-col w_button"> <label>Order No:</label> <asp:TextBox runat="server" ID="txtorderno"></asp:TextBox>  </div>
			<div class="input-col w_button" style="margin-left:168px; margin-top:-5px;"> 
                
                <asp:Button runat="server" Text="Open Workflow Folder"  OnClick="Unnamed_Click" />
                </div>
			<div class="input-col"> <label>Distributor:</label> <asp:TextBox runat="server" ID="txtdistributor"></asp:TextBox> </div>
			
			<div class="input-col"> <label>Branch:</label> <asp:TextBox runat="server" ID="txtbranch"></asp:TextBox> </div>
			
			<div class="input-col"> <label>PO NO:</label> <asp:TextBox runat="server" ID="txtpono"></asp:TextBox> </div>
			
			 <div class="selection_box">
				   <label>Document Type:</label>
				 <asp:DropDownList runat="server" ID="ddldocumenttypeorderdocument"></asp:DropDownList>
			</div>
			
			 <div class="selection_box">
				   <label>Additional Info:</label>
				  <asp:DropDownList runat="server" ID="ddladditionalinfo"></asp:DropDownList>
			</div>			
		
		</div>
		
		
		
		<div class="order_box_p2" style="width:60%;">
		  <h2 class="file_path">File Name</h2>
		  
		  <div class="files_placed">  
		    <p class="file_name" id="file_nameid" runat="server"></p>
		  </div>
		  <div style="height:40px; float:left"></div>
		  <div id="dropOnMe" draggable="false">
              <div style="float:left;height:60px; width:100%"></div>
              <p style="text-align:center">Drag and Drop Files Here</p>
		  </div>
		  
		</div>

         <div style="height:30px; float:left;width:100%"> </div>
         
       <div class="order_button">
      <asp:Button runat="server" Text="Process File" ID="upload" stlye="float:left" OnClick="button_submit"  />
     </div>
	
     </div>  
	 
        


 </section>
   
 
   
 </form>
</body>
</html>
