web3.eth.defaultAccount = web3.eth.accounts[0];

    
// --Decrypting the block for the transactions
// Requires Abi decoder for NPM

//Declare the Contract Body
var ContractAbi = web3.eth.contract([{ 'constant': false, 'inputs': [{ 'name': '_id', 'type': 'uint256' }, { 'name': '_fileid', 'type': 'uint256' }, { 'name': '_fileHash', 'type': 'string' }, { 'name': '_date', 'type': 'string' }], 'name': 'AddFiles', 'outputs': [], 'payable': false, 'stateMutability': 'nonpayable', 'type': 'function' }, { 'anonymous': false, 'inputs': [{ 'indexed': false, 'name': 'id', 'type': 'uint256' }, { 'indexed': false, 'name': 'fileid', 'type': 'uint256' }, { 'indexed': false, 'name': 'fileHash', 'type': 'string' }, { 'indexed': false, 'name': 'date', 'type': 'string' }], 'name': 'FileUploadEvent', 'type': 'event' }]);

//Declare the decoder abi
const DecoderAbi = [{ 'inputs': [{ 'name': '_id', 'type': 'uint256' }, { 'name': '_fileid', 'type': 'uint256' }, { 'name': '_fileHash', 'type': 'string' }, { 'name': '_date', 'type': 'string' }], 'name': 'AddFiles', 'outputs': [], 'payable': false, 'stateMutability': 'nonpayable', 'type': 'function' }, { 'anonymous': false, 'inputs': [{ 'indexed': false, 'name': 'id', 'type': 'uint256' }, { 'indexed': false, 'name': 'fileid', 'type': 'uint256' }, { 'indexed': false, 'name': 'fileHash', 'type': 'string' }, { 'indexed': false, 'name': 'date', 'type': 'string' }], 'name': 'FileUploadEvent', 'type': 'event' }];

//Add DecoderAbi to the AbiDecoder
abiDecoder.addABI(DecoderAbi);

//Declare the contract Address
var ImageContract = ContractAbi.at('0x538882ec49974f8815fee55ad7b40d6dd4b6b75d');

//Logs decrypted by abi
var datalistdecrypted = [];

var images = [{}];
var jsonImage = new Array();

var count = web3.eth.getTransactionCount("0x198e13017d2333712bd942d8b028610b95c363da");

for (var i = 0; i < web3.eth.getTransactionCount("0x198e13017d2333712bd942d8b028610b95c363da") ; i++) {

    var block = web3.eth.getBlock(web3.eth.blockNumber - i);
    var reciept = web3.eth.getTransactionReceipt(block["transactions"][0]);
    if (reciept["logs"].length != 0)
    {
        datalistdecrypted[i] = abiDecoder.decodeLogs(reciept["logs"]);
        images[i] = datalistdecrypted[i][0]["events"];

        if (images[i][0]["value"] == $("#id").val()) {
            jsonImage.push(new Object({
                id: images[i][0]["value"],
                fileid: images[i][1]["value"],
                filehash: images[i][2]["value"],
                date: images[i][3]["value"]
            }));
        }

    }
}

var data = JSON.stringify(jsonImage);

$.ajax({
    url: "ValidateImages",
    type: "POST",
    data: { "data":  JSON.stringify(jsonImage) },
    success: function (JTransaction) {
        if (JTransaction)

            for (var i = 0; i < JTransaction["JTransaction"].length; i++) {
                
                $("#transactiontable").append("<tr><td>" + JTransaction["JTransaction"][i]["id"] + "</td><td>" + JTransaction["JTransaction"][i]["filename"] + "</td> <td>  <img src=/images/" + JTransaction["JTransaction"][i]["status"] + " style='width:50px; height:50px'> </td> <td>" + JTransaction["JTransaction"][i]["date"] + "</td> </tr>");

            }
     
}
});