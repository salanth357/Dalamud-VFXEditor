using System.Collections.Generic;
using System.IO;
using VfxEditor.Formats.MdlFormat.Utils;
using VfxEditor.Parsing;
namespace VfxEditor.Formats.MdlFormat.Bone.V6 {
    public class MdlBoneTableV6 : MdlBoneTable {
        public MdlBoneTableV6( BinaryReader reader, List<string> boneStrings, int count ) : base() {
            for( var i = 0; i < count; i++ ) {
                var boneIndex = reader.ReadInt16();
                var bone = new ParsedString( "##Name" ) {
                    Value = boneIndex < boneStrings.Count ? boneStrings[boneIndex] : "[ERROR]"
                };
                Bones.Add( bone );
            }
        }

        public override void Write( BinaryWriter writer, MdlWriteData data ) {
            foreach( var item in Bones ) writer.Write( ( ushort )data.StringTable.BoneStrings.IndexOf( item.Value ) );
        }
    }
}
