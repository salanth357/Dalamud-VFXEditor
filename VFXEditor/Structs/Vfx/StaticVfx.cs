using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Dalamud.Game.ClientState.Actors.Types;
using Dalamud.Plugin;

using ImGuizmoNET;

namespace VFXEditor.Structs.Vfx {
    public unsafe class StaticVfx : BaseVfx {

        public StaticVfx( Plugin plugin, string path, Vector3 position) : base( plugin, path ) {
            Vfx = Plugin.ResourceLoader.StaticVfxCreate( path, "Client.System.Scheduler.Instance.VfxObject" );
            Plugin.ResourceLoader.StaticVfxRun( Vfx, 0.0f, 0xFFFFFFFF );

            UpdatePosition( position );
            Update();
            UpdateMatrix();
        }

        public override void Remove() {
            Plugin.ResourceLoader.StaticVfxRemove( Vfx );
        }
    }
}