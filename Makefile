BUILD=xbuild
MONO=mono
PROJECTS=netcdf4.net.csproj netcdf4.net.test_suite.csproj
SOURCES=.//ASA/NetCDF4/NcArray.cs .//ASA/NetCDF4/NcAtt.cs .//ASA/NetCDF4/NcByte.cs .//ASA/NetCDF4/NcChar.cs .//ASA/NetCDF4/NcCheck.cs .//ASA/NetCDF4/NcCompound.cs .//ASA/NetCDF4/NcCompoundType.cs .//ASA/NetCDF4/NcDim.cs .//ASA/NetCDF4/NcDouble.cs .//ASA/NetCDF4/NcEnumType.cs .//ASA/NetCDF4/NcException.cs .//ASA/NetCDF4/NcFile.cs .//ASA/NetCDF4/NcFloat.cs .//ASA/NetCDF4/NcGroup.cs .//ASA/NetCDF4/NcGroupAtt.cs .//ASA/NetCDF4/NcInt.cs .//ASA/NetCDF4/NcInt64.cs .//ASA/NetCDF4/NcOpaqueType.cs .//ASA/NetCDF4/NcShort.cs .//ASA/NetCDF4/NcString.cs .//ASA/NetCDF4/NcType.cs .//ASA/NetCDF4/NcUbyte.cs .//ASA/NetCDF4/NcUint.cs .//ASA/NetCDF4/NcUint64.cs .//ASA/NetCDF4/NcUshort.cs .//ASA/NetCDF4/NcVar.cs .//ASA/NetCDF4/NcVarAtt.cs .//ASA/NetCDF4/NcVlenType.cs .//ASA/NetCDF4/NetCDF.cs .//Properties/AssemblyInfo.cs .//RunTests.cs .//Test/Assert.cs .//Test/TestData.cs .//Test/TestHelper.cs .//Test/TestNcArray.cs .//Test/TestNcAtt.cs .//Test/TestNcDim.cs .//Test/TestNcFile.cs .//Test/TestNcGroup.cs .//Test/TestNcType.cs .//Test/TestNcVar.cs .//Test/unittest.cs

bin/Debug/netcdf4.net.dll: netcdf4.net.csproj $(SOURCES)
	$(BUILD) netcdf4.net.csproj

bin/Debug/nc-test.exe: netcdf4.net.test_suite.csproj $(SOURCES)
	$(BUILD) netcdf4.net.test_suite.csproj

test: bin/Debug/netcdf4.net.dll bin/Debug/nc-test.exe
	LD_LIBRARY_PATH=/usr/local/netcdf/4.2.1.1/lib:/usr/local/hdf5/1.8.11/lib $(MONO) bin/Debug/nc-test.exe

clean:
	rm -rf bin/Debug

