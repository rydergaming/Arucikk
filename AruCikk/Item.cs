using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AruCikk
{
    public class Item
    {
        private string _itemName;
        public string ItemName
        {
            get
            {
                return _itemName;
            }
            set
            {
                _itemName = value;
            }
        }
        private string _itemNo;
        public string ItemNo
        {
            get
            {
                return _itemNo;
            }
            set
            {
                _itemNo = value;
            }
        }
        private string _barcode;
        public string Barcode
        {
            get
            {
                return _barcode;
            }
            set
            {
                _barcode = value;
            }
        }
        private string _unit;
        public string Unit
        {
            get
            {
                return _unit;
            }
            set
            {
                _unit = value;
            }
        }

    }
}
