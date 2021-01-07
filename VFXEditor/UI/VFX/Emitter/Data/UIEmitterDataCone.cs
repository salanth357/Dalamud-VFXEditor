using AVFXLib.Models;
using ImGuiNET;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VFXEditor.UI.VFX
{
    public class UIEmitterDataCone : UIBase
    {
        public AVFXEmitterDataCone Data;
        //==========================

        public UIEmitterDataCone( AVFXEmitterDataCone data )
        {
            Data = data;
            //=======================
            Attributes.Add( new UICombo<RotationOrder>( "Rotation Order", Data.RotationOrderType ) );
            Attributes.Add( new UICurve( Data.OuterSize, "Outer Size" ) );
            Attributes.Add( new UICurve( Data.InjectionSpeed, "Injection Speed" ) );
            Attributes.Add( new UICurve( Data.InjectionAngle, "Injection Angle" ) );
        }

        public override void Draw( string parentId )
        {
            string id = parentId + "/Data";
            DrawAttrs( id );
        }
    }
}
