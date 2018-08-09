using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EDIT5;
namespace ReJS.CS
{
    class Syntetic
    {
        //пилим котлин метод
        //fun hello(name: String = "world"): String {}
        void T_FUNC_Kotlin(BLOCK_CS P_BL)//-> fun
        {
            var func_name= SET_KWq();
            FUNC F = CREATE_Func(P_BL, func_name);
                INK();
                T_PAR_KOTLIN(F);
            if (NOT(":")) ADD_ERR();
                SET_END_ROW_EXTAN(GetChep , EXTAN.TYPE_ONLY);
                INK();
                F.TYPE_W5=T_W5();
                TAKE_FUNC_BODY(F);
        }
        void T_PAR_KOTLIN(FUNC F)// -> (
        {
            chepperTxt S;
            if (NOT("(")) ADD_ERR();
            aaa:
            var Name = INK();
            if (Name.t == ")") { INK(); return; }
                S=INK();
            if (S.t!=":") ADD_ERR();
                SET_END_ROW_EXTAN(S,EXTAN.TYPE_ONLY);
                INK();
                var W5=T_W5();
                var P=F.AddPar(W5,Name);
                if (T("="))
                    {
                        INK();P.DefValue = E2();
                    }
            if (T(",")) goto aaa;
            if (NOT(")")) ADD_ERR();
            INK();
        }
    }

    //класс отвечающий за содержимое открывающего меню
    public static class EXTAN
    {
  
        public const int END_OF_ROW_IN_CLASS = 120; // public static...
        public const int END_OF_ROW_IN_BLOCK = 121; // NO public,static
        public const int END_OF_ROW_IN_NAMESPACE = 122; // public static...
        public const int NEW_USING = 123;

        public const int CLASS_ONLY = 124; ///inherit
                        public const int TYPE_ONLY  = 125;// пока никто не юзает

        public const int FUNC_PAR_0 = 126; //fucn( this...
        public const int FUNC_PAR_1 = 127; //fucn( params...
        public const int TYPE_VAR = 128;
        public const int GOTO = 130;

        public const int SWITCH_CASE_DEF = 130;
        public const int SWITCH_CASE_DEF_VAL = 131;

        public const int CATCH_FIN_ONLY = 132;
        public const int CATCH_2 = 133;

        //--public const int DELEGATE_ONLY = 134;

        public const int UN_USE1 = 230;
        public const int UN_USE2 = 231;
        public const int UN_USE3 = 232;
        public const int UN_USE4 = 233;
        public const int UN_USE5 = 234;

        public const int TABLE_SQL_TYPES = 400;
        public const int TABLE_SQL_AfterName = 401;

        public const int extentional_class = 555;
    }
    public  class EXTAN_OPEN
    {
      
        public menuForm MENU_EL;
        public FILE_CS THE_FILE;
        editor EDITOR;
        static string[] TYPES = new string[] {"class","struct","delegate","enum"};
        static string[] PROT  = new string[] { "public", "private", "protected", "internal" };
        static void ADD2(List<iKinderName> lst, params object[] names)
        {
            foreach (var q in names)
            {
                var S = q as string[];
                if (S != null)
                {
                    ADDD(lst, S);
                    continue;
                }
            }
        }
        static void ADDD(List<iKinderName> lst, params string[] names)
        {
            foreach (var q in names)
                lst.Add(new iKindNameStr() { Name = new chepperTxt() { t = q } });
        }
        void NSP_ONLY(List<iKinderName> lst)
        {
            var a1 = THE_FILE.PRJ.DIC_CL.Values.ToList();
            foreach (var a2 in a1)
            {
                if (a2.BLOCK_KIND == HOLDER_ENUM.NSP) lst.Add(a2);
            }
        }
      
        object OpenMenu(List<iKinderName> lst)=> OP_MENU_CL.OpenMenu(MENU_EL,lst, EDITOR);
            
        
        void TYPE_UP(List<iKinderName> lst, BLOCK_CS BL,EN_LOOK what)
        {
            FIND_EL.LookInMe2(BL,lst, what, EN_LOOK_PROT.PRIV, false,null);
            lst.AddRange( FIND_EL.TAKE_ALL_FROM_USING(THE_FILE));
            MEGA.LOOK_TYPES_IN_ALL_PRJ(lst);
            
        }
        void CLASS_ONLY(List<iKinderName> lst, BLOCK_CS BL)=> TYPE_UP(lst, BL, EN_LOOK.CLASS);
        void TYPE_ONLY(List<iKinderName> lst, BLOCK_CS BL) => TYPE_UP(lst, BL, EN_LOOK.TYPE);
        void TYPE_AND_VAR(List<iKinderName> lst, BLOCK_CS BL)
        {
            TYPE_UP(lst, BL, EN_LOOK.TYPE_STAT_AND_NONSTAT);
            lst.AddRange(THE_FILE.PRJ.DIC_FV.Values);
        }
        
        public object OPEN(menuForm menu, EDIT5.editor ED)
        {

            MENU_EL = menu;
            EDITOR = ED;

            var C0 = OP_MENU_CL.GET_CURRENT_CHEP(ED, -1);

            THE_FILE = EDITOR.THE_FILE;/
            var off = EDITOR.OFF_SET;
            

            var lst = new List<iKinderName>();
            
            if (C0 == null)//первое слово
            {
              
                ADDD(lst,  "using", "namespace");
                TYPE_ONLY(lst, THE_FILE);
                return OpenMenu(lst);
            }
            var t = C0.t;
            var BL = OP_MENU_CL.FIND_BLOCK(THE_FILE, C0.Location);
            switch (t)
            {
                case "namespace":
                case "using":  // немного есть разница. в новом шарпе можно прицепить статический класс
                    NSP_ONLY(lst);
                    return OpenMenu(lst);
                    
                case "=":
                    var kk = C0.PrevChepper.IKND;
                    if (kk!=null)
                    {
                        var attr_name_rt = kk.GET_RT.LOOK_NEXT(new Search_option() { search_name = C0.PrevChepper.t, WHAT= EN_LOOK.NONSTAT });
                        if (attr_name_rt.Count > 0)
                        {
                            var res2=   attr_name_rt[0].GET_RT.LOOK_NEXT(new Search_option() { WHAT = EN_LOOK.NONSTAT });
                            return OpenMenu(res2);
                        }
                    }
                    TYPE_AND_VAR(lst, BL); return OpenMenu(lst);
                    
            }

            
            switch (C0.EXTAN)
            {

                case EXTAN.extentional_class: ADDD(lst, "int", "__string__","__double__","bool","char"); return OpenMenu(lst);

                case EXTAN.UN_USE3: var s2 = C0.QQ as List<iKinderName>;  return OpenMenu(s2);

                case EXTAN.UN_USE5: var s = C0.QQ as string[]; ADDD(lst, s); return  OpenMenu(lst);

                case EXTAN.TABLE_SQL_TYPES:           ADDD(lst,"int", "string", "bool");    return OpenMenu(lst);
                case EXTAN.TABLE_SQL_AfterName: ADDD(lst, "identity", "notnull"); return OpenMenu(lst);

                case EXTAN.UN_USE4:
                   
                    var TUP  = C0.QQ as Line5_Tuple;
                    
                    RTYPE WT = null;

                    if (TUP==null) return null;
                    var PARENT = TUP.PARENT_TUPLE;
                    if (PARENT!=null)
                    {
                        WT = TUP?.PARENT_TUPLE_NAME?.IKND?.GET_RT;
                    }
                    else
                       WT=FIND_T.FIND_TYPE(THE_FILE, BL, TUP.TUPLE_CLASS_TYPE);
                    if (WT!=null) lst = WT.LOOK_NEXT(THE_FILE, EN_LOOK.NONSTAT, EN_LOOK_PROT.PUB);

                    foreach(var q3 in TUP.Names) if (q3 != null) lst.RemoveAll(x => x.Name.t == q3.t);
                    //foreach(var q in lst)
                    


                    return OpenMenu(lst);
             

                case EXTAN.CATCH_FIN_ONLY:        ADDD(lst, "finally", "catch"); return OpenMenu(lst);

                case EXTAN.CATCH_2:               TYPE_AND_VAR(lst, BL); goto case EXTAN.CATCH_FIN_ONLY;
                                                  
                case EXTAN.SWITCH_CASE_DEF:       ADDD(lst, "case", "default"); return OpenMenu(lst);   

                case EXTAN.SWITCH_CASE_DEF_VAL:   TYPE_AND_VAR(lst, BL);
                                                  ADDD(lst, "case", "default"); return OpenMenu(lst);
                case EXTAN.TYPE_VAR:              TYPE_AND_VAR(lst, BL);        return OpenMenu(lst);
                    
                case EXTAN.TYPE_ONLY:             TYPE_ONLY(lst, BL);           return OpenMenu(lst);
                    
                case EXTAN.FUNC_PAR_0:            ADDD(lst, "this");            goto case EXTAN.FUNC_PAR_1;

                case EXTAN.FUNC_PAR_1:            TYPE_ONLY(lst, BL);           ADDD(lst, "params","ref","out");          return  OpenMenu(lst);
                    
                case EXTAN.CLASS_ONLY:
                    CLASS_ONLY(lst, BL);
                    return OpenMenu(lst);
                    
                case EXTAN.NEW_USING:
                    ADD2(lst, TYPES, PROT);
                    ADDD(lst, "var", "using", "namespace");
                    lst.AddRange(THE_FILE.PRJ.DIC_CL.Values);
                    return OpenMenu(lst);
                    
                case EXTAN.END_OF_ROW_IN_NAMESPACE:  TYPE_ONLY(lst, BL);       ADD2(lst, TYPES, PROT); ADDD(lst, "namespace");  return OpenMenu(lst);
                    
                case EXTAN.END_OF_ROW_IN_BLOCK: // внутри метода
                    TYPE_AND_VAR(lst, BL);
                    ADDD(lst, "for", "foreach", "while", "do");
                    return OpenMenu(lst);
                    
                 case EXTAN.END_OF_ROW_IN_CLASS:
                    TYPE_ONLY(lst, BL);
                    ADD2(lst, TYPES, PROT);
                    return  OpenMenu(lst);
                    

               
            }
            return null;
        }
    }
}
