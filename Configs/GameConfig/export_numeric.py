import os
import pandas as pd
import sys
Xlsx_File = './Datas/NumericDesc.xlsx'

def get_numeric_desc():
  
    df = pd.read_excel(Xlsx_File, sheet_name="Sheet1")
    return df

def export_numeric_desc():
    df = get_numeric_desc()
    #前四行不要
    df = df.iloc[4:]
    #遍历
    list = []
    for i in range(len(df)):
        #获取每一行数据
        it = df.iloc[i]
        note = it["#note"]
        id = it["id"]
        name = it["name"]
        base = it["base"]
        add = it["add"]
        pct = it["pct"]
        finalAdd = it["finalAdd"]
        finalPct = it["finalPct"]
    
        list.append(
            {
                "id":id,
                "name":name,
                "base":base,
                "add":add,
                "pct":pct,
                "finalAdd":finalAdd,
                "finalPct":finalPct,
                "note" : note
            }
        )
    
    write_code(list)


Code_Path = "./"
def write_code(list : list):
    strList = []
    strList.append("// 自动生成代码，不要修改")
    strList.append("namespace GameConfig")
    strList.append("{")
    strList.append("\tpublic static class NumericType")
    strList.append("\t{")
    strList.append("\t\t public const int Max = 10000;")
    for item in list:
        strList.append(f"\t\t//{item['id']} - {item['name']}  {item['note']} ")
        strList.append(f"\t\tpublic const int {item['name']} = {item['id']};")
        if item["base"] == 1:
            strList.append(f"\t\tpublic const int {item['name']}Base = {item['name']} * 10 + 1;")
        if item["add"] == 1:
            strList.append(f"\t\tpublic const int {item['name']}Add = {item['name']} * 10 + 2;")
        if item["pct"] == 1:
            strList.append(f"\t\tpublic const int {item['name']}Pct = {item['name']} * 10 + 3;")
        if item["finalAdd"] == 1:
            strList.append(f"\t\tpublic const int {item['name']}FinalAdd = {item['name']} * 10 + 4;")
        if item["finalPct"] == 1:
            strList.append(f"\t\tpublic const int {item['name']}FinalPct = {item['name']} * 10 + 5;")
    strList.append("\t}")
    strList.append("}")

    code = "\n".join(strList)
    # 写入文件
    with open(Code_Path + "NumericType.cs", "w+", encoding="utf-8") as f:
        f.write(code)



#主函数

if __name__ == "__main__":
    print("开始导出数值类型")
    #Code_Path = 传入参数
    if len(sys.argv) > 1:
        Code_Path = sys.argv[1]
    print(Code_Path)
    export_numeric_desc()