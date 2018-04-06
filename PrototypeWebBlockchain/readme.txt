1. install node js
2. In Windows Powershell or cmd type npm install -g ganache-cli
3. After installing ganache run this command  ganache-cli --gasPrice 6 --gasLimit 9999999999 --db "C:\Ganache-Cli\Dump" --account="0x7231a774a538fce22a329729b03087de4cb4a1119494db1c10eae3bb491823e7,1000000000000000000000000000000000000000000000000000000000000000" 
4. Run the project. 

Notes
-First you must register then upload image .
-If the transactionlist is not responding check the ganache-cli if it froze.

Installing the contract

1. Go to remix
2. In run tab click environment and then choose web3.Provider
3. type localhost:8545
4. copy and paste the sol contract in the remix.
5. In the run tab click create.
6. You can now save and view files that was saved in the transaction.