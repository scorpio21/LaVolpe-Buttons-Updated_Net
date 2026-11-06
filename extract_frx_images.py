"""Extrae las imágenes incrustadas en frmSample.frx (VB6) a PNG.

Uso:
    python extract_frx_images.py [origen.frx] [carpeta-destino]

Sin argumentos usa ../LaVolpe-Buttons-Updated-/frmSample.frx y
LaVolpe.Buttons.Net/Resources, relativos a este script.

Requiere Pillow (pip install pillow). Solo se usa como herramienta auxiliar:
el repositorio no depende de ella para compilar.
"""

from __future__ import annotations

import argparse
import io
from pathlib import Path

from PIL import Image

# Offsets hexadecimales observados en frmSample.frm (referencias Picture del FRX).
OFFSETS = {"image0": 0x1719, "image1": 0x1D98, "image2": 0x7C5A}
HEADER = 12  # cabecera del registro de imagen de VB6 antes del payload


def extract(source: Path, destination: Path) -> int:
    data = source.read_bytes()
    destination.mkdir(parents=True, exist_ok=True)
    extracted = 0
    for name, offset in OFFSETS.items():
        payload = data[offset + HEADER :]
        try:
            with Image.open(io.BytesIO(payload)) as image:
                image.load()
                target = destination / f"{name}.png"
                image.convert("RGBA").save(target)
                print(f"{name}: offset=0x{offset:X} formato={image.format} tamaño={image.size} -> {target}")
                extracted += 1
        except Exception as exc:  # algunos registros son DIB/icono sin cabecera completa
            print(f"{name}: payload no soportado: {exc}")
    return extracted


def main() -> int:
    root = Path(__file__).resolve().parent
    parser = argparse.ArgumentParser(description="Extrae las imágenes de frmSample.frx a PNG.")
    parser.add_argument("source", nargs="?", type=Path, default=root / "LaVolpe-Buttons-Updated-" / "frmSample.frx")
    parser.add_argument("destination", nargs="?", type=Path, default=root / "LaVolpe.Buttons.Net" / "Resources")
    args = parser.parse_args()

    if not args.source.is_file():
        parser.error(f"no existe el FRX: {args.source}")
    count = extract(args.source, args.destination)
    print(f"Extraídas {count} de {len(OFFSETS)} imágenes.")
    return 0 if count else 1


if __name__ == "__main__":
    raise SystemExit(main())
