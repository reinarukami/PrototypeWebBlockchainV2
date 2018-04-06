
//Declare Account to be used
web3.eth.defaultAccount = web3.eth.accounts[0];

options = {
    gasPrice: '5', // default gasPrice when
    gasLimit: '999999999' // default gasLimit when
}

//Declare the Contract Body
var ContractAbi = web3.eth.contract();

//Declare the contract Address
var ImageContract = ContractAbi.at('0x61cf48bdcc8b07881566cdaa299389788b0db1a0');

//var count = ImageContract.countInstructors();

//for (i = 1; i <= count; i++) {
//    var result = contract.getMember(i);
//    if (result[0] != '') {
//        $('#membertable').append('<tr id='' + i + 'tr'><td>' + result[0] + '</td>' + '<td>' + result[1] + '</td>' + '<td>' + result[2] + '</td>' + '<td>' + result[3] + '</td>' + '<td>' + result[4] + '</td> <td> <button type='button' class='btn btn-warning' id='' + i + '' onclick='getID(this.id)'>EDIT</button> <button type='button' class='btn btn-danger' id='' + i + '' onclick='Delete(this.id)'>Delete</button> </td> </tr>');
//    }
//}

console.log($('#hash').val());
var test = $('#hash').val()
ImageContract.AddFiles(1, $('#hash').val(), '12/12/2018', { gas: 999999 });

//$('#button2').click(function () {
//    load();
//    contract.UpdateMember(modifyID, $('#fname').val(), $('#lname').val(), $('#age').val(), $('#contact').val(), $('#address').val(), { gas: 999999 });
//});

//function Delete(id) {
//    load();
//    contract.DeleteMember(id, { gas: 999999 });
//}

//var tcount = 0;
//var MemberEvent = contract.MemberEvent();
//MemberEvent.watch(function (error, result) {
//    if (!error && tcount != 0) {
//        if (result.args.proc == 'AddMember') {
//            $('#membertable').append('<tr id='' + result.args.id + 'tr'><td>' + result.args.fname + '</td>' + '<td>' + result.args.lname + '</td>' + '<td>' + result.args.age + '</td>' + '<td>' + result.args.contactnumber + '</td>' + '<td>' + result.args.home + '</td> <td> <button type='button' class='btn btn-warning' id='' + result.args.id + '' onclick='getID(this.id)'>EDIT</button> <button type='button' class='btn btn-danger' id='' + result.args.id + '' onclick='Delete(this.id)'>Delete</button> </td> </tr>');
//            loadfinish();
//        }

//        else if (result.args.proc == 'UpdateMember') {
//            $('#' + result.args.id + 'tr').children().remove();
//            $('#' + result.args.id + 'tr').append('<td>' + result.args.fname + '</td>' + '<td>' + result.args.lname + '</td>' + '<td>' + result.args.age + '</td>' + '<td>' + result.args.contactnumber + '</td>' + '<td>' + result.args.home + '</td> <td> <button type='button' class='btn btn-warning' id='' + result.args.id + '' onclick='getID(this.id)'>EDIT</button> <button type='button' class='btn btn-danger' id='' + result.args.id + '' onclick='Delete(this.id)'>Delete</button> </td>');
//            $('#button').show();
//            $('#update-control').hide();
//            loadfinish();
//        }

//        else if (result.args.proc == 'DeleteMember') {
//            $('#' + result.args.id + 'tr').remove()
//            loadfinish();
//        }
//    }
//    else {
//        tcount++;
//        $('loader').hide();
//        console.log('error');
//    }
//})

web3.eth.getTransactionReceipt(0xd4b14e18c5b34100eb9c0eb29ccdc2b9dfb55b7657c20add7819f53d091ccd32, function (err, receipt) {
        if (err) {
            error(err);
        }

        if (receipt !== null) {
            // Transaction went through
            if (cb) {
                cb(receipt);
            }
        } else {
            // Try again in 1 second
            window.setTimeout(function () {
                waitForReceipt(0xd4b14e18c5b34100eb9c0eb29ccdc2b9dfb55b7657c20add7819f53d091ccd32, cb);
            }, 1000);
        }
    });
