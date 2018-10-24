namespace Matlab.DataModel
{
    public enum FactorStatus
    {
        PreSend,
        Sended,
        SendingError,
        Redirected,
        CallbackRecived,
        CallbackRecivedWithError,
        VerifyError,
        Finished
    }
}