using System;
using System.Collections.Generic;
using System.Text;
using QuizConfiguratorV2.Dialogs;

namespace QuizConfiguratorV2.Services
{
    public class DialogService
    {
        public string? ShowCreateNewPackDialog()
        {
            var dialog = new CreateNewPackDialog();

            bool? result = dialog.ShowDialog();

            if (result == true)
            {
                return dialog.PackName;
            }
            
                return null;
            }
        }

    }
