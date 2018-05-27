namespace Receipt.Repository
{
	public interface IReceiptRepository
    {
        IReceipt CreateReceipt(IReceipt newReceipt);
        void DeleteReceipt(int id);
        IReceipt GetReceipt(int id);
        IReceipt GetReceipt(string code);
        IReceipt UpdateReceipt(IReceipt existingReceipt, IReceipt newReceipt);
        string GenerateNewCode(int len = 6);
    }
}
