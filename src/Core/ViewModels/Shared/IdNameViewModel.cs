namespace ViewModels.Shared;

public class IdNameViewModel<TIdentity> : object
{
    #region Constants
    public const string DataValueField = "Id";
    public const string DataTextField = "Name";
    #endregion /Constants

    #region Constructors
    public IdNameViewModel() : base()
    {
    }

    public IdNameViewModel(string? title) : this()
    {
        Title = title;
    }

    public IdNameViewModel
        (TIdentity id, string? keyName) : this()
    {
        Id = id;
        KeyName = keyName;
    }
    #endregion /Constructors

    #region Properties

    public string? Title { get; set; }

    public string? KeyName { get; set; }

    public int Ordering { get; set; }

    public TIdentity? Id { get; set; }

    #endregion /Properties

    #region Read Only Properties

    public string? Name
    {
        get
        {
            if (string.IsNullOrWhiteSpace(value: Title) == false)
            {
                return Title;
            }

            return KeyName;
        }
    }

    #endregion /Read Only Properties
}
