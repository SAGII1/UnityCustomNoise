using System.Collections.Generic;

namespace noise.module
{
	public class Module {

        protected List<Module> _sourceModules;

        // public Module(int count) {

        // }


        // public Module GetSourceModule(int index) {
        //     return null;
        // }

        virtual public int GetSourceModuleCount()
		{
			return 0;
		}

        virtual public double GetValue(double x, double y, double z)
		{
			return 0d;
		}
    }
}