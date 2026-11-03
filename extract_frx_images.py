from pathlib import Path
from PIL import Image
import io

src = Path(r'K:\Proyectos\LaVolpe-Buttons-Updated\LaVolpe-Buttons-Updated-\frmSample.frx')
out = Path(r'K:\Proyectos\LaVolpe-Buttons-Updated\LaVolpe.Buttons.Net\Resources')
out.mkdir(exist_ok=True)
data = src.read_bytes()
found = []
# VB6 FRX references observed in frmSample.frm are hexadecimal offsets.
for name, offset in [('image0', 0x1719), ('image1', 0x1D98), ('image2', 0x7C5A)]:
    payload = data[offset + 12:]
    try:
        with Image.open(io.BytesIO(payload)) as im:
            im.load()
            path = out / f'{name}.png'
            im.convert('RGBA').save(path)
            found.append((name, offset, im.format, im.size, path))
    except Exception as exc:
        # Some VB6 records contain a DIB/icon payload without a complete file header.
        print(f'{name}: unsupported direct image payload: {exc}')
for row in found:
    print(f'{row[0]} offset=0x{row[1]:X} format={row[2]} size={row[3]} path={row[4]}')
