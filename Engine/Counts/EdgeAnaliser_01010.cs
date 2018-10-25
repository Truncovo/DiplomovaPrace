using System;

namespace Engine.Counts
{
    public partial class EdgeAnaliser
    {
    

        private void From11To10()
        {
            _result.AddFirst.Add(_points[_actualMin].Copy());
            _result.InContactFirst.Add(false);

            _onSecondEdge = false;


        }

        private void From11To01()
        {
            _result.AddSecond.Add(_points[_actualMin].Copy());
            _result.InContactSecond.Add(false);

            _onFirstEdge = false;


        }

        private void From10To11()
        {
            _result.AddFirst.Add(_points[_actualMin].Copy());
            _result.InContactFirst.Add(true);

            _result.InContactSecond.Add(true);

            _onSecondEdge = true;

            UpdateActualMin();

        }

        private void From01To11()
        {
            _result.AddSecond.Add(_points[_actualMin].Copy());
            _result.InContactSecond.Add(true);

            _result.InContactFirst.Add(true);

            _onFirstEdge = true;

            UpdateActualMin();

        }

        private void From00To010()
        {
            if (IsFromFirst(_actualMin))
            { 
                _onFirstEdge = true;
                _result.InContactFirst.Add(false);
            }
            else
            {
                _onSecondEdge = true;
                _result.InContactSecond.Add(false);
            }
            UpdateActualMin();
        }
    }
}