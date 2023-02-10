class Account    {
	private Object thisLock = new Object();
	int balance;
	
    public int Withdraw(int amount)        
	{
	    lock (thisLock) {
            balance -= amount;
        }
        return balance;
    }
}

internal class BankAccount
{
    public int Balance;
}