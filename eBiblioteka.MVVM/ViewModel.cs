﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eBiblioteka.MVVM
{
    public abstract class ViewModel : IDataErrorInfo
    {
        public string this[string columnName] => OnValidate(columnName);

        public string Error
        {
            get
            {
                throw new NotSupportedException();
            }
        }

        protected virtual string OnValidate(string propertyName)
        {
            var context = new ValidationContext(this)
            {
                MemberName = propertyName
            };

            var results = new Collection<ValidationResult>();
            var isValid = Validator.TryValidateObject(this, context, results, true);
            if (!isValid)
            {
                ValidationResult result = results.SingleOrDefault(p =>
                                                                  p.MemberNames.Any(memberName =>
                                                                                    memberName == propertyName));
                return result == null ? null : result.ErrorMessage;
            }
            return null;
        }
    }
}
