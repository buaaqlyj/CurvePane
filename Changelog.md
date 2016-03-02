****
# CurvePane_vx.x.x

## Changelog

* Tweak the names of some classes.
* Add members to ICurvePointList.
* Implement the Parametric Cubic Spline Interpolation Curve.
* Fix the bug that drawing duplicated curves that connect base points.

****
# CurvePane_v0.6.23

## Release Note

I've finished the Cubic Spline Interpolation Curve and done a lot of small tweaks of other parts.

## Changelog

* Tweak the names of some methods.
* Add static members to DoubleExtension.
* Add file drag&drop support for base points import.
* Implement the Cubic Spline Interpolation Curve.

****
# CurvePane_v0.5.19

## Release Note

I've finished the NURBS Curve and done a lot of small tweaks of other parts.

## Changelog

* Implement a feature that it'll tweak the axis range of the pane after every drawing.
* Fix the bug that cause the "Divided by zero" report.
* Add the notification of the min and max degree that one B-Spline curve can have for reference.
* Lock the node vector textbox if the node is set to be distributed uniformly.
* Standardize the name of classes.
* Implement the ArrayExtension to help calculate the maximum count of one element in a list or an array.
* Remove the minimum degree calculation from B-Spline and NURBS curve because it's wrong.
* Fix the bug in ToString method of LagarangeIntervalPolynomialCurveElement.
* Implement the NURBS Curve.

****
# CurvePane_v0.4.10

## Release Note

I've finished the B-Spline Curve.

## Changelog

* Implemented the B-Spline Curve.

****
# CurvePane_v0.3.9-alpha

## Release Note

I've finished the Bezier Curve.

## Changelog

* Implemented the Bezier Curve.
* Implemented the feature that the coordinate of the mouse is displayed on the program when the mouse is on the GraphPane.

****
# CurvePane_v0.2.7

## Release Note

I've finished the Newton Polynomial Interpolation.

## Changelog

* Implemented the Newton Polynomial Interpolation.
* Implemented the CurveName manager which can update the curve name automately after every draw.

****
# CurvePane_v0.1.5

## Release Note

I've finished the Lagarange Polynomial Interpolation.

## Changelog

* Implemented the Lagarange Polynomial Interpolation.
* Extracted some classes to individual libraries.

****
# CurvePane_v0.0.1

## Release Note

No public API is available yet. But I have finished most part of the curve processing procedure, so I decided to publish it as v0.0.1 to mark the first step of this project.

## Changelog

* Implemented the data processing procedure of Lagrange Linear Interpolation;
* Implemented most of the interfaces used in curve data structures;
* Implemented the layout of the GUI;

****
# CurvePane_Readme

The final project for my Product Modeling Tech class

## License

>  Copyright © 2016 Troy Lewis. Some Rights Reserved
>
>  Licensed under the Apache License, Version 2.0 (the "License");
>  you may not use this file except in compliance with the License.
>  You may obtain a copy of the License at
>
>      http://www.apache.org/licenses/LICENSE-2.0
>
>  Unless required by applicable law or agreed to in writing, software
>  distributed under the License is distributed on an "AS IS" BASIS,
>  WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
>  See the License for the specific language governing permissions and
>  limitations under the License.

The ZedGraph control library which is under LGPLv2 is used in this program without any modifications.

## Requirements

* DotNet Framework 2.0

## Introduction

https://github.com/buaaqlyj/CurvePane

This program can draw some curve splines which are commonly used in Product Modeling, including: Polynomial curves, CSI curve(Cubic Spline Interpolation), Parametric Cubic Spline curve, Bézier curve, B-Spline curve and NURBS curve.