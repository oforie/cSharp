
function getData(){
    $.get("/method", function(res){
         $(".refreshcount").html("Passcode # " + res.refreshCount);
        $(".passcode").html(res.passcode);
        console.log("This is the data i am sending your way", res), "Json"
    });
   


}


$(document).ready(function(){
   $("button").click(function(){
       getData(data => {
           console.log("This is the data", data);
           
       });
   });

});