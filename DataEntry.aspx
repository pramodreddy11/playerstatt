<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DataEntry.aspx.cs" Inherits="Theplayerstats.com.DataEntry" %>


<html>
<head runat="server">
    <title></title>
    
    <script src="Scripts/jquery-3.5.1.js"></script>
    <style>
        input {border : 1px solid black;margin:0; font-size:20px; }
input:focus,input:active,input:hover { outline: 1px solid #eee; background-color:#eee; }

table { border : 1px solid #999; border-collapse:collapse;border-spacing:0; }
table td { padding:0; margin:0;border:1px solid #999; }
table th { background-color: #aaa; min-width:20px;border:1px solid #999; }


        table, tr, th ,td{
            border:1px solid black;
            border-collapse:collapse
        }

        #results{
            margin-top:100px;
            margin-left:100px
        }
    </style>
       
   
</head>
<body>
    <form id="form1" class="" runat="server">
        <div>
            <asp:Label ID="Label1" runat="server" Text="Please insert country Name :"></asp:Label>
            <asp:TextBox ID="CountryName" runat="server" autocomplete="off"></asp:TextBox>
            <asp:Button ID="AddDetails" runat="server" Text="Add Country" OnClick="Add_country" />
            <asp:Button ID="GetCountryDetails" runat="server" Text="Get Details" />
        </div>
    <br /> <br /> <br /> <br /> <br />

     <div>
        
            <asp:Label ID="Label2" runat="server" Text="Select Country"></asp:Label>
            <asp:DropDownList ID="DropDownList1" runat="server"></asp:DropDownList>
       <asp:Label ID="Label3" runat="server" Text="StadiumName"></asp:Label>
           <asp:TextBox ID="StadiumName" runat="server" autocomplete="off"></asp:TextBox>
            <asp:Button ID="AddStadium" runat="server" Text="Add Stadium" OnClick="Add_stadium" />
            <asp:Button ID="GetStadiumDetails" runat="server" Text="Get Details" />
          <br /> <br /> <br /> <br /> <br />
    </div>

        <div>

             <asp:Label ID="Label4" runat="server" Text="Select Country"></asp:Label>
            <asp:DropDownList ID="DropDownList2" runat="server"></asp:DropDownList>
           
       <asp:Label ID="Label5" runat="server" Text="PlayerName"></asp:Label>
           <asp:TextBox ID="TextBox1" runat="server" autocomplete="off"></asp:TextBox>
            <asp:Button ID="Button1" runat="server" Text="Add Player" OnClick="Add_Player" />
            <asp:Button ID="GetPlayerDetails" runat="server" Text="Get Details" />

        </div>

        </form>




    <div id="results">



    </div>
</body>
     <script>


         $("#GetPlayerDetails").click(function (e) {
             e.preventDefault();
             var index = $("#DropDownList2 option:selected").val();
             $.ajax({

                 type: "GET",
                 url: "DataEntry.aspx/GetPlayerDetailss",
                 data: { index: index },
                 contentType: "application/json; charset=utf-8",
                 //  dataType: "json",
                 success: function (result) {
                     var obj = $.parseJSON(result.d);
                  

                     $("#results").empty();
                     var markup = '';
                     var finalmarkup = '';
                     var header = '';
                     var count = 1;

                     for (var i = 0; i < obj.length; i++) {

                         var countryID = obj[i].CountryID;
                         var PlayerName = obj[i].PlayerName;
                         var PlayerId = obj[i].PlayerID;
                         if (count == 1)
                             header = "<tr><th>PlayerID</th><th>PlayerName</th><th>CountryID</th><th>Actions</th></tr>";
                         markup += "<tr><td>" + PlayerId + "</td>"+"<td>" + countryID + "</td>" + "<td>"+ PlayerName + "</td><td><button onclick = 'PlayerDelete(this);'>Delete</button>" + "<button onclick = 'EditPlayer(this);'>Edit</button></td>"+"</tr>";
                         count++;
                     }
                     finalmarkup = "<table>" + header + markup + "</table>";
                     $("#results").append(finalmarkup);

                 }

             });
         }
         );

         function EditPlayer(ctl) {
             var value = $(ctl).parents("tr")[0].cells[2].innerHTML;
             $(ctl).parents("tr")[0].cells[2].innerHTML = "<input type=\"text\" class =\"Edit\" value =\"" + value + "\"/>";
             $(ctl).parents("tr")[0].cells[3].innerHTML = "<button  onclick = 'SavePlayerDetails(this);'>Save</button>";
         }

         function SavePlayerDetails(details) {

             //var savedetailsvalue = $(details).parents("tr")[0].cells[2].innerHTML;
             var Playername = $(".Edit").val();
             var PlayerID = $(details).parents("tr")[0].cells[0].innerHTML;
             var CountryID = $(details).parents("tr")[0].cells[1].innerHTML
            // var value = $(ctl).parents("tr")[0].cells[1].innerHTML;
             var obj = {
                 PlayerName: Playername,
                 PlayerID: PlayerID,
                 CountryID: CountryID
             };
             var json = JSON.stringify(obj);
             $.ajax({

                 type: "POST",
                 url: "DataEntry.aspx/SavePlayerDetails",
                 data: json,
                 contentType: "application/json; charset=utf-8",
                 dataType: "json",
                 success: function (result) {
                     $("#GetPlayerDetails").trigger('click');
                 }

             });
             
         }

         function CountryDelete(ctl) {
             if (confirm('Are You Sure ???')) {


                 var value = $(ctl).parents("tr")[0].cells[0].innerHTML;
                 $.ajax({

                     type: "get",
                     url: "DataEntry.aspx/DeleteCountry",
                     data: { countryId: value },
                     contentType: "application/json; charset=utf-8",
                     dataType: "json",
                     success: function (result) {
                         $("#GetCountryDetails").trigger('click');
                     }

                 });
             }
         }

         function StadiumDelete(ctl) {
             if (confirm('Are You Sure ???')) {


                 var value = $(ctl).parents("tr")[0].cells[0].innerHTML;
                 //value = "pramod";
                 $.ajax({

                     type: "get",
                     url: "DataEntry.aspx/DeleteStadium",
                     data: { StadiumId: value },
                     contentType: "application/json; charset=utf-8",
                     dataType: "json",
                     success: function (result) {
                         $("#GetStadiumDetails").trigger('click');
                     }

                 });
             }

         }

         function PlayerDelete (ctl) {
             if (confirm('Are You Sure ???')) {


                 var value = $(ctl).parents("tr")[0].cells[2].innerHTML;
                 var obj = {
                     PlayerName: value,
                     
                 };
                 var json = JSON.stringify(obj);
                
                 $.ajax({

                     type: "POST",
                     url: "DataEntry.aspx/PlayersDelete",
                     data:  json,
                     contentType: "application/json; charset=utf-8",
                     dataType: "json",
                     success: function (result) {
                         $("#GetPlayerDetails").trigger('click');
                     }

                 });
             }
         }

        



         $("#GetCountryDetails").click(function (e) {
             e.preventDefault();
             console.log("hai");
             $.ajax({

                 type: "GET",
                 url: "DataEntry.aspx/GetDetails",
                 // data: "{empid: empid}",
                 contentType: "application/json; charset=utf-8",
                 //  dataType: "json",
                 success: function (result) {
                     var obj = $.parseJSON(result.d);


                     $("#results").empty();
                     var markup = '';
                     var finalmarkup = '';
                     var header = '';
                     var count = 1;

                     for (var i = 0; i < obj.length; i++) {

                         var countryID = obj[i].CountryID;
                         var countryName = obj[i].CountryName;
                         if (count == 1)
                             header = "<tr><th>CountryID</th><th>CountryName</th><th>Actions</th></tr>";
                         markup += "<tr><td>" + countryID + "</td>" + "<td>" + countryName + "</td>" + "<td><button onclick = 'CountryDelete(this);'>Delete</button>" +"<button onclick = 'EditCountry(this);'>Edit</button></td>"+"</tr>";
                         count++;
                     }
                     finalmarkup = "<table>" + header + markup + "</table>";
                     $("#results").append(finalmarkup);

                 }

             });
         }
         );




         $("#GetStadiumDetails").click(function (e) {
             e.preventDefault();
             var index = $("#DropDownList1 option:selected").val();
            
             $.ajax({

                 type: "get",
                 url: "DataEntry.aspx/GetSDetails",
                 data: { index: index},
                 contentType: "application/json; charset=utf-8",
                 dataType: "json",
                 success: function (result) {
                     var obj = $.parseJSON(result.d);
                     $("#StadiumName").val('');
                     $("#results").empty();
                     var markup = '';
                     var finalmarkup = '';
                     var header = '';
                     var count = 1;

                     for (var i = 0; i < obj.length; i++) {

                         var StadiumID = obj[i].StadiumID;
                         var StadiumName = obj[i].StadiumName;
                         var CountryID = obj[i].CountryID;
                         if (count == 1)
                             header = "<tr><th>StadiumID</th><th>StadiumName</th></tr>";
                         markup += "<tr><td>" + StadiumID + "</td>" + "<td>" + StadiumName + "</td>" + "<td>" + CountryID + "</td><td><button onclick = 'StadiumDelete(this);'>Delete</button>" + "<button onclick = 'Editstadium(this);'>Edit</button></td>"+"</tr>";
                         count++;
                     }
                     finalmarkup = "<table>" + header + markup + "</table>";
                     $("#results").append(finalmarkup);

                 }

             });
         }
         );




    </script>
 
</html>
