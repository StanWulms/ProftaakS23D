﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace IventWeb
{
    public partial class Reservatie : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Session["loadpage"] = "true";
            Session["loadpagebetaling"] = "true";
        }
    }
}