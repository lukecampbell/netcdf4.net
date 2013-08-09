#!/usr/bin/env python
# Author: Luke Campbell
# File: template.py
# Usage template.py <template-file> [output-file]

import sys
import os
types_list = [
        ('sbyte'  , 'schar'     , 'NcByte'   , 'NC_BYTE'   , 'Sbyte')  ,
        ('byte'   , 'uchar'     , 'NcUbyte'  , 'NC_UBYTE'  , 'Byte')   ,
        ('Int16'  , 'short'     , 'NcShort'  , 'NC_SHORT'  , 'Int16')  ,
        ('UInt16' , 'ushort'    , 'NcUshort' , 'NC_USHORT' , 'UInt16') ,
        ('Int32'  , 'int'       , 'NcInt'    , 'NC_INT'    , 'Int32')  ,
        ('UInt32' , 'uint'      , 'NcUint'   , 'NC_UINT'   , 'UInt32') ,
        ('Int64'  , 'longlong'  , 'NcInt64'  , 'NC_INT64'  , 'Int64')  ,
        ('UInt64' , 'ulonglong' , 'NcUint64' , 'NC_UINT64' , 'UInt64') ,
        ('float'  , 'float'     , 'NcFloat'  , 'NC_FLOAT'  , 'Float')  ,
        ('double' , 'double'    , 'NcDouble' , 'NC_DOUBLE' , 'Double') ,
        ]

def file_template(filepath, outpath):
    with open(filepath, 'r') as f, open(outpath, 'w') as out_file:

        block_queue = []
        while True:
            buf = f.readline()
            if not buf:
                break
            if buf == '%%\n':
                block_queue.append('')
            elif buf == '/%%\n':
                block = block_queue.pop()
                if block_queue:
                    for t, m, i, c, u in types_list:
                        block_queue[-1] += block % dict(t=t, l=t.lower(), m=m, i=i, c=c, u=u)
                else:
                    for t, m, i, c, u in types_list:
                        out_file.write(block % dict(t=t, l=t.lower(), m=m, i=i, c=c, u=u))
            elif block_queue:
                block_queue[-1] += buf
            else:
                out_file.write(buf)


def main():
    if len(sys.argv) > 2:
        outpath = sys.argv[2]
    elif len(sys.argv) == 2:
        f = os.path.basename(sys.argv[1])
        f = f.replace('.i', '.cs')
        d = os.path.dirname(sys.argv[1])
        outpath = os.path.join(d,f)
    else:
        print_usage()


    file_template(sys.argv[1], outpath)

def print_usage():
    print 'python template.py <template-file> [output-file]'

if __name__ == '__main__':
    main()


