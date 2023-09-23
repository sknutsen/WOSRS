using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WOSRSTest.Shared.Models.Interfaces;

namespace WOSRSTest.Shared.DataContainers.Interfaces;

public interface IContainer
{
    public IEntityClass ToEntityClass();
    public void Fill(IEntityClass entity);
}
