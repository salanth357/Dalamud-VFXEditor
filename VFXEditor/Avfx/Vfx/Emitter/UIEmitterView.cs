using AVFXLib.Models;
using System;
using System.Linq;
using AVFXLib.AVFX;

namespace VFXEditor.Avfx.Vfx {
    public class UIEmitterView : UIDropdownView<UIEmitter> {
        public UIEmitterView( AvfxFile main, AVFXBase avfx ) : base( main, avfx, "##EMIT", "Select an Emitter", defaultPath: "emitter_default.vfxedit" ) {
            Group = main.Emitters;
            Group.Items = AVFX.Emitters.Select( item => new UIEmitter( Main, item ) ).ToList();
        }

        public override void OnDelete( UIEmitter item ) {
            AVFX.RemoveEmitter( item.Emitter );
        }

        public override byte[] OnExport( UIEmitter item ) {
            return item.Emitter.ToAVFX().ToBytes();
        }

        public override UIEmitter OnImport( AVFXNode node, bool has_dependencies = false ) {
            var item = new AVFXEmitter();
            item.Read( node );
            AVFX.AddEmitter( item );
            return new UIEmitter( Main, item, has_dependencies );
        }
    }
}
