namespace Engine.Shapes
{
    public interface ICalculatable  
    {
        double Plocha { get; }
        string Header { get; }
        double LambdaGround { get; }
        double TepelnyOdporSkladby { get; }
        double EfektivniTepelnyOdporZeminy { get; }
        double EkvTloustka { get; }
        double ExponovanyObvodPodlahy { get; }
        double CharRozmerPodlahy { get; }
        bool JePodlahaDobreIzolovana { get; }
        bool JePodlahaSpatneIzolovana { get; }

        double SoucinitelProstupuTepla { get; }
        double TepelnyTok { get; }
        double VazenyPrumerLinearnichCinitelu { get; }
        double VazenyPrumerLinearnichCiniteluPredDelenim { get; }
        double VazenyPrumerTlSteny { get; }
        double VazenyPrumerTlStenyPredDelenim { get; }
        double PrumernySoucinitelProstupTepla { get; }
    }
}