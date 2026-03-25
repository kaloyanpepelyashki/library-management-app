namespace LibraryManagementApp.ApplicationLogic.Interfaces;

public interface IBorrower
{
    bool ReturnBook(int bookId);
    bool BorrowBook(int bookId);
    bool RateBook(int bookId);
}