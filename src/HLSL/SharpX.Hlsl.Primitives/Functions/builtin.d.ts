/**
 * Scalar<T> expands to T.
 * Example:
 *   Scalar<float> expands to float
 */
type Scalar<T> = {};

/**
 * Vector<T> expends to T, T2, T3, and T4.
 * Example:
 *   Vector<float> expands to float, float2, float3, float4
 *
 *  If you specify x such as Vector<float, 3>, it expands to float3
 */
type Vector<T, T1 extends number = any> = {};

/**
 * Matrix<T> expands to T2x2, T2x3, ...and others.
 * Example:
 *   Matrix<float> expands to float2x2, float2x3, ..., float4x4
 *
 * If you specify x and y such as Matrix<float, 1, 2>, it expands to float1x2
 */
type Matrix<T, T1 extends number = any, T2 extends number = any> = {};

type Out<T> = {};

type int = {};
type uint = {};
type float = {};
type double = {};
type Sampler1D = {};
type Sampler2D = {};
type Sampler3D = {};
type SamplerCUBE = {};

export type Builtin = {
  abort(): void;

  abs<T extends Scalar<float | int>>(x: T): T;
  abs<T extends Vector<float | int>>(x: T): T;
  abs<T extends Matrix<float | int>>(x: T): T;

  acos(x: Scalar<float>): Scalar<float>;
  acos(x: Vector<float>): Vector<float>;
  acos(x: Matrix<float>): Matrix<float>;

  all(x: Scalar<float | int | boolean>): Scalar<boolean>;
  all(x: Vector<float | int | boolean>): Scalar<boolean>;
  all(x: Matrix<float | int | boolean>): Scalar<boolean>;

  any(x: Scalar<float | int | boolean>): Scalar<boolean>;
  any(x: Vector<float | int | boolean>): Scalar<boolean>;
  any(x: Matrix<float | int | boolean>): Scalar<boolean>;

  asin(x: Scalar<float>): Scalar<float>;
  asin(x: Vector<float>): Vector<float>;
  asin(x: Matrix<float>): Matrix<float>;

  atan(x: Scalar<float>): Scalar<float>;
  atan(x: Vector<float>): Vector<float>;
  atan(x: Matrix<float>): Matrix<float>;

  atan(y: Scalar<float>, x: Scalar<float>): Scalar<float>;
  atan(y: Vector<float>, x: Vector<float>): Vector<float>;
  atan(y: Matrix<float>, x: Matrix<float>): Matrix<float>;

  ceil(x: Scalar<float>): Scalar<float>;
  ceil(x: Vector<float>): Vector<float>;
  ceil(x: Matrix<float>): Matrix<float>;

  clamp<T extends Scalar<float | int>>(x: T, min: T, max: T): T;
  clamp<T extends Vector<float | int>>(x: T, min: T, max: T): T;
  clamp<T extends Matrix<float | int>>(x: T, min: T, max: T): T;

  clip(x: Scalar<float>): void;
  clip(x: Vector<float>): void;
  clip(x: Matrix<float>): void;

  cos(x: Scalar<float>): Scalar<float>;
  cos(x: Vector<float>): Vector<float>;
  cos(x: Matrix<float>): Matrix<float>;

  cosh(x: Scalar<float>): Scalar<float>;
  cosh(x: Vector<float>): Vector<float>;
  cosh(x: Matrix<float>): Matrix<float>;

  cross(x: Vector<float, 3>, y: Vector<float, 3>): Vector<float, 3>;

  ddx(x: Scalar<float>): Scalar<float>;
  ddx(x: Vector<float>): Vector<float>;
  ddx(x: Matrix<float>): Matrix<float>;

  ddx_coarse(x: Scalar<float>): Scalar<float>;
  ddx_coarse(x: Vector<float>): Vector<float>;

  ddx_fine(x: Scalar<float>): Scalar<float>;
  ddx_fine(x: Vector<float>): Vector<float>;

  ddy(x: Scalar<float>): Scalar<float>;
  ddy(x: Vector<float>): Vector<float>;
  ddy(x: Matrix<float>): Matrix<float>;

  ddy_coarse(x: Scalar<float>): Scalar<float>;
  ddy_coarse(x: Vector<float>): Vector<float>;

  ddy_fine(x: Scalar<float>): Scalar<float>;
  ddy_fine(x: Vector<float>): Vector<float>;

  degrees(x: Scalar<float>): Scalar<float>;
  degrees(x: Vector<float>): Vector<float>;
  degrees(x: Matrix<float>): Matrix<float>;

  determinant(x: Matrix<float, 2, 2>): Matrix<float, 2, 2>;
  determinant(x: Matrix<float, 3, 3>): Matrix<float, 3, 3>;
  determinant(x: Matrix<float, 4, 4>): Matrix<float, 4, 4>;

  distance(x: Vector<float>, y: Vector<float>): Scalar<float>;

  dot(x: Vector<float>, y: Vector<float>): Scalar<float>;
  dot(x: Vector<int>, y: Vector<int>): Scalar<int>;

  exp(x: Scalar<float>): Scalar<float>;
  exp(x: Vector<float>): Vector<float>;
  exp(x: Matrix<float>): Matrix<float>;

  exp2(x: Scalar<float>): Scalar<float>;
  exp2(x: Vector<float>): Vector<float>;
  exp2(x: Matrix<float>): Matrix<float>;

  faceforward(
    n: Vector<float>,
    i: Vector<float>,
    ng: Vector<float>
  ): Vector<float>;

  floor(x: Scalar<float>): Scalar<float>;
  floor(x: Vector<float>): Vector<float>;
  floor(x: Matrix<float>): Matrix<float>;

  fmod(x: Scalar<float>, y: Scalar<float>): Scalar<float>;
  fmod(x: Vector<float>, y: Vector<float>): Vector<float>;
  fmod(x: Matrix<float>, y: Matrix<float>): Matrix<float>;

  frac(x: Scalar<float>): Scalar<float>;
  frac(x: Vector<float>): Vector<float>;
  frac(x: Matrix<float>): Matrix<float>;

  frexp(x: Scalar<float>, exp: Out<Scalar<float>>): Scalar<float>;
  frexp(x: Vector<float>, exp: Out<Vector<float>>): Vector<float>;
  frexp(x: Matrix<float>, exp: Out<Matrix<float>>): Matrix<float>;

  fwidth(x: Scalar<float>): Scalar<float>;
  fwidth(x: Vector<float>): Vector<float>;
  fwidth(x: Matrix<float>): Matrix<float>;

  isfinite(x: Scalar<float>): Scalar<boolean>;
  isfinite(x: Vector<float>): Vector<boolean>;
  isfinite(x: Matrix<float>): Matrix<boolean>;

  isinf(x: Scalar<float>): Scalar<boolean>;
  isinf(x: Vector<float>): Vector<boolean>;
  isinf(x: Matrix<float>): Matrix<boolean>;

  isnan(x: Scalar<float>): Scalar<boolean>;
  isnan(x: Vector<float>): Vector<boolean>;
  isnan(x: Matrix<float>): Matrix<boolean>;

  ldexp(x: Scalar<float>, exp: Scalar<float>): Scalar<float>;
  ldexp(x: Vector<float>, exp: Vector<float>): Vector<float>;
  ldexp(x: Matrix<float>, exp: Matrix<float>): Matrix<float>;

  length(x: Vector<float>): Scalar<float>;

  lerp(x: Scalar<float>, y: Scalar<float>, s: Scalar<float>): Scalar<float>;
  lerp(x: Vector<float>, y: Vector<float>, s: Vector<float>): Vector<float>;
  lerp(x: Matrix<float>, y: Matrix<float>, s: Matrix<float>): Matrix<float>;

  lit(
    n_dot_l: Scalar<float>,
    n_dot_h: Scalar<float>,
    m: Scalar<float>
  ): Vector<float, 4>;

  log(x: Scalar<float>): Scalar<float>;
  log(x: Vector<float>): Vector<float>;
  log(x: Matrix<float>): Matrix<float>;

  log10(x: Scalar<float>): Scalar<float>;
  log10(x: Vector<float>): Vector<float>;
  log10(x: Matrix<float>): Matrix<float>;

  log2(x: Scalar<float>): Scalar<float>;
  log2(x: Vector<float>): Vector<float>;
  log2(x: Matrix<float>): Matrix<float>;

  max<T extends Scalar<float | int>>(x: T, y: T): T;
  max<T extends Vector<float | int>>(x: T, y: T): T;
  max<T extends Matrix<float | int>>(x: T, y: T): T;

  min<T extends Scalar<float | int>>(x: T, y: T): T;
  min<T extends Vector<float | int>>(x: T, y: T): T;
  min<T extends Matrix<float | int>>(x: T, y: T): T;

  modf<T extends Scalar<float | int>>(x: T, ip: Out<T>): T;
  modf<T extends Vector<float | int>>(x: T, ip: Out<T>): T;
  modf<T extends Matrix<float | int>>(x: T, ip: Out<T>): T;

  // mul

  noise(x: Vector<float>): Scalar<float>;

  normalize(x: Vector<float>): Vector<float>;

  pow(x: Scalar<float>, y: Scalar<float>): Scalar<float>;
  pow(x: Vector<float>, y: Vector<float>): Vector<float>;
  pow(x: Matrix<float>, y: Matrix<float>): Matrix<float>;

  radians(x: Scalar<float>): Scalar<float>;
  radians(x: Vector<float>): Vector<float>;
  radians(x: Matrix<float>): Matrix<float>;

  rcp(x: Scalar<float>): Scalar<float>;
  rcp(x: Vector<float>): Vector<float>;
  rcp(x: Matrix<float>): Matrix<float>;

  reflect(i: Vector<float>, n: Vector<float>): Vector<float>;

  refract(i: Vector<float>, n: Vector<float>, q: Scalar<float>): Vector<float>;

  round(x: Scalar<float>): Scalar<float>;
  round(x: Vector<float>): Vector<float>;
  round(x: Matrix<float>): Matrix<float>;

  rsqrt(x: Scalar<float>): Scalar<float>;
  rsqrt(x: Vector<float>): Vector<float>;
  rsqrt(x: Matrix<float>): Matrix<float>;

  saturate(x: Scalar<float>): Scalar<float>;
  saturate(x: Vector<float>): Vector<float>;
  saturate(x: Matrix<float>): Matrix<float>;

  sign<T extends Scalar<float | int>>(x: T): T;
  sign<T extends Vector<float | int>>(x: T): T;
  sign<T extends Matrix<float | int>>(x: T): T;

  sin(x: Scalar<float>): Scalar<float>;
  sin(x: Vector<float>): Vector<float>;
  sin(x: Matrix<float>): Matrix<float>;

  sincos(
    x: Scalar<float>,
    s: Out<Scalar<float>>,
    c: Out<Scalar<float>>
  ): Scalar<float>;
  sincos(
    x: Vector<float>,
    s: Out<Vector<float>>,
    c: Out<Vector<float>>
  ): Vector<float>;
  sincos(
    x: Matrix<float>,
    s: Out<Matrix<float>>,
    c: Out<Matrix<float>>
  ): Matrix<float>;

  sinh(x: Scalar<float>): Scalar<float>;
  sinh(x: Vector<float>): Vector<float>;
  sinh(x: Matrix<float>): Matrix<float>;

  smoothstep(
    x: Scalar<float>,
    min: Scalar<float>,
    max: Scalar<float>
  ): Scalar<float>;
  smoothstep(
    x: Vector<float>,
    min: Vector<float>,
    max: Vector<float>
  ): Vector<float>;
  smoothstep(
    x: Matrix<float>,
    min: Matrix<float>,
    max: Matrix<float>
  ): Matrix<float>;

  sqrt(x: Scalar<float>): Scalar<float>;
  sqrt(x: Vector<float>): Vector<float>;
  sqrt(x: Matrix<float>): Matrix<float>;

  step(x: Scalar<float>, y: Scalar<float>): Scalar<float>;
  step(x: Vector<float>, y: Vector<float>): Vector<float>;
  step(x: Matrix<float>, y: Matrix<float>): Matrix<float>;

  tan(x: Scalar<float>): Scalar<float>;
  tan(x: Vector<float>): Vector<float>;
  tan(x: Matrix<float>): Matrix<float>;

  tanh(x: Scalar<float>): Scalar<float>;
  tanh(x: Vector<float>): Vector<float>;
  tanh(x: Matrix<float>): Matrix<float>;

  tex1D(s: Sampler1D, t: Scalar<float>): Vector<float, 4>;
  tex1D(
    s: Sampler1D,
    t: Vector<float, 1>,
    ddx: Vector<float, 1>,
    ddy: Vector<float, 1>
  ): Vector<float, 4>;
  tex1Dbias(s: Sampler1D, t: Vector<float, 4>): Vector<float, 4>;
  tex1Dgrad(
    s: Sampler1D,
    t: Vector<float, 1>,
    ddx: Vector<float, 1>,
    ddy: Vector<float, 1>
  ): Vector<float, 4>;
  tex1Dlod(s: Sampler1D, t: Vector<float, 4>): Vector<float, 4>;
  tex1Dproj(s: Sampler1D, t: Vector<float, 4>): Vector<float, 4>;

  tex2D(s: Sampler2D, t: Vector<float, 2>): Vector<float, 4>;
  tex2D(
    s: Sampler2D,
    t: Vector<float, 2>,
    ddx: Vector<float, 2>,
    ddy: Vector<float, 2>
  ): Vector<float, 4>;
  tex2Dbias(s: Sampler2D, t: Vector<float, 4>): Vector<float, 4>;
  tex2Dgrad(
    s: Sampler2D,
    t: Vector<float, 2>,
    ddx: Vector<float, 2>,
    ddy: Vector<float, 2>
  ): Vector<float, 4>;
  tex2Dlod(s: Sampler2D, t: Vector<float, 4>): Vector<float, 4>;
  tex2Dproj(s: Sampler2D, t: Vector<float, 4>): Vector<float, 4>;

  tex3D(s: Sampler3D, t: Vector<float, 3>): Vector<float, 4>;
  tex3D(
    s: Sampler3D,
    t: Vector<float, 3>,
    ddx: Vector<float, 3>,
    ddy: Vector<float, 3>
  ): Vector<float, 4>;
  tex3Dbias(s: Sampler3D, t: Vector<float, 4>): Vector<float, 4>;
  tex3Dgrad(
    s: Sampler3D,
    t: Vector<float, 3>,
    ddx: Vector<float, 3>,
    ddy: Vector<float, 3>
  ): Vector<float, 4>;
  tex3Dlod(s: Sampler3D, t: Vector<float, 4>): Vector<float, 4>;
  tex3Dproj(s: Sampler3D, t: Vector<float, 4>): Vector<float, 4>;

  texCUBE(s: SamplerCUBE, t: Vector<float, 3>): Vector<float, 4>;
  texCUBE(
    s: SamplerCUBE,
    t: Vector<float, 3>,
    ddx: Vector<float, 3>,
    ddy: Vector<float, 3>
  ): Vector<float, 4>;
  texCUBEbias(s: SamplerCUBE, t: Vector<float, 4>): Vector<float, 4>;
  texCUBEgrad(
    s: SamplerCUBE,
    t: Vector<float, 3>,
    ddx: Vector<float, 3>,
    ddy: Vector<float, 3>
  ): Vector<float, 4>;
  texCUBElod(s: SamplerCUBE, t: Vector<float, 4>): Vector<float, 4>;
  texCUBEproj(s: SamplerCUBE, t: Vector<float, 4>): Vector<float, 4>;

  // transpose

  trunc(x: Scalar<float>): Scalar<float>;
  trunc(x: Vector<float>): Vector<float>;
  trunc(x: Matrix<float>): Matrix<float>;
};
