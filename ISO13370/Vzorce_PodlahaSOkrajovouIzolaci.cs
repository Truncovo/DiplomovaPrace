using System;


public partial class Vzorce
{
    //dsc - přídavná efektivní tloustka
    //Rsc - přídavný tepelný odpor okrajové izolace
    //Rn - tepelný odpor vodorovné nebo svislé izolace
    //dn - tloustka vodorovmné nebo svislé izolace

    public double F8_dsc(double Rsc, double lambda)
    {
        return Rsc* lambda;
    }

    public double F9_Rsc(double Rn, double dn, double lambda)
    {
        return Rn - (dn / lambda);
    }
    //D - šířka vodorovné izolace
    //platí pro vodorovnou přídavnou izolaci
    public double F10_deltaPsi(double lambda, double D, double dsc, double dt)
    {
        double ln1 = Math.Log(D/dt + 1);
        double ln2 = Math.Log(D / (dt + dsc) + 1);
        return -(lambda / Math.PI) * (ln1 - ln2);
    }
    public double F11_deltaPsi(double lambda, double D, double dsc, double dt)
    {
        double ln1 = Math.Log(2* D / dt + 1);
        double ln2 = Math.Log(2* D / (dt + dsc) + 1);
        return -(lambda / Math.PI) * (ln1 - ln2);
    }

}