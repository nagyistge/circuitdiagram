﻿// Namespaces.cs
//
// Circuit Diagram http://www.circuit-diagram.org/
//
// Copyright (C) 2012  Sam Fisher
//
// This program is free software; you can redistribute it and/or
// modify it under the terms of the GNU General Public License
// as published by the Free Software Foundation; either version 2
// of the License, or (at your option) any later version.
//
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
//
// You should have received a copy of the GNU General Public License
// along with this program; if not, write to the Free Software
// Foundation, Inc., 51 Franklin Street, Fifth Floor, Boston, MA  02110-1301, USA.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CircuitDiagram.IO.CDDX
{
    /// <summary>
    /// Stores namespaces that are used in CDDX files.
    /// </summary>
    public static class Namespaces
    {
        /// <summary>
        /// The XML namespace used for the main document.
        /// </summary>
        public const string Document = "http://schemas.circuit-diagram.org/circuitDiagramDocument/2012/document";

        /// <summary>
        /// The XML namespace used for references to component descriptions within the main document.
        /// </summary>
        public const string DocumentComponentDescriptions = "http://schemas.circuit-diagram.org/circuitDiagramDocument/2012/document/component-descriptions";

        /// <summary>
        /// The XML namespace used for OPC relationships.
        /// </summary>
        public const string Relationships = "http://schemas.openxmlformats.org/officeDocument/2006/relationships";

        /// <summary>
        /// The namespace for the Dublin Core elements.
        /// </summary>
        public const string DublinCore = "http://purl.org/dc/terms/";

        /// <summary>
        /// The namespace for the Dublin Core Terms elements.
        /// </summary>
        public const string DublinCoreTerms = "http://purl.org/dc/terms/";
    }
}
