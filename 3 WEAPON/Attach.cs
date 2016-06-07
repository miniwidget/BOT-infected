using InfinityScript;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Infected
{
    /// <summary>
    /// player's attachment index class
    /// </summary>
    class Attach
    {
        int ar = 0;
        public int AR_BEFORE = 0;
        public int AR_AFTER
        {
            get
            {
                AR_BEFORE = this.ar;
                this.ar++;
                if (this.ar > 4)
                {
                    AR_BEFORE = 4;
                    this.ar = 0;
                }
                return this.ar;
            }
            set
            {
                this.ar = value;
            }
        }

        int lm = 0;
        public int LM_BEFORE = 0;
        public int LM_AFTER
        {
            get
            {
                LM_BEFORE = this.lm;
                this.lm++;
                if (this.lm > 2) this.lm = 0;
                return this.lm;
            }set
            {
                this.lm = 0;
            }
        }

        int att = 0;
        public int SIRENCERorHB
        {
            get
            {
                this.att++;
                if (this.att > 2) this.att = 0;
                return this.att;
            }
        }

    }
}
