
ADDIN_BUILD = $(top_builddir)/build/AddIns/BackendBindings
ASSEMBLY = $(ADDIN_BUILD)/CBinding.dll

REFS =  \
	-pkg:glib-sharp-2.0 \
	-pkg:gtk-sharp-2.0 \
	-r:../../build/AddIns/MonoDevelop.Deployment.dll \
	-r:../../build/AddIns/MonoDevelop.Deployment.dll \
	-r:../../build/AddIns/MonoDevelop.Deployment.Linux.dll \
	-r:../../build/AddIns/MonoDevelop.Deployment.Linux.dll \
	-r:../../build/AddIns/MonoDevelop.SourceEditor.dll \
	-r:../../build/AddIns/MonoDevelop.SourceEditor.dll \
	-r:../../build/bin/MonoDevelop.Components.dll \
	-r:../../build/bin/MonoDevelop.Components.dll \
	-r:../../build/bin/MonoDevelop.Core.dll \
	-r:../../build/bin/MonoDevelop.Core.dll \
	-r:../../build/bin/MonoDevelop.Core.Gui.dll \
	-r:../../build/bin/MonoDevelop.Core.Gui.dll \
	-r:../../build/bin/MonoDevelop.Ide.dll \
	-r:../../build/bin/MonoDevelop.Ide.dll \
	-r:../../build/bin/MonoDevelop.Projects.dll \
	-r:../../build/bin/MonoDevelop.Projects.dll \
	-r:../../build/bin/MonoDevelop.Projects.Gui.dll \
	-r:../../build/bin/MonoDevelop.Projects.Gui.dll \
	-r:../../contrib/Mono.Addins.dll \
	-r:Mono.Posix \
	-r:System \
	-r:System.Xml
	

FILES =  \
	CLanguageBinding.cs \
	Compiler/CCompiler.cs \
	Compiler/GccCompiler.cs \
	Compiler/GNUCompiler.cs \
	Compiler/GppCompiler.cs \
	Compiler/ICompiler.cs \
	CppLanguageBinding.cs \
	gtk-gui/CBinding.AddLibraryDialog.cs \
	gtk-gui/CBinding.AddPathDialog.cs \
	gtk-gui/CBinding.CodeGenerationPanel.cs \
	gtk-gui/CBinding.CompilerPanel.cs \
	gtk-gui/CBinding.EditPackagesDialog.cs \
	gtk-gui/CBinding.OutputOptionsPanel.cs \
	gtk-gui/generated.cs \
	Gui/AddLibraryDialog.cs \
	Gui/AddPathDialog.cs \
	Gui/CodeGenerationPanel.cs \
	Gui/CompilerPanel.cs \
	Gui/CTextEditorExtension.cs \
	Gui/DataProvider.cs \
	Gui/EditPackagesDialog.cs \
	Gui/OutputOptionsPanel.cs \
	Navigation/ClassNodeBuilder.cs \
	Navigation/ClassPadEventArgs.cs \
	Navigation/EnumerationNodeBuilder.cs \
	Navigation/EnumeratorNodeBuilder.cs \
	Navigation/FunctionNodeBuilder.cs \
	Navigation/GlobalsNodeBuilder.cs \
	Navigation/LanguageItemCommandHandler.cs \
	Navigation/LanguageItemEventArgs.cs \
	Navigation/MacroDefinitionsNodeBuilder.cs \
	Navigation/MacroNodeBuilder.cs \
	Navigation/MemberNodeBuilder.cs \
	Navigation/NamespaceNodeBuilder.cs \
	Navigation/ProjectNodeBuilderExtension.cs \
	Navigation/StructureNodeBuilder.cs \
	Navigation/TypedefNodeBuilder.cs \
	Navigation/UnionNodeBuilder.cs \
	Navigation/VariableNodeBuilder.cs \
	Parser/Class.cs \
	Parser/Enumeration.cs \
	Parser/Enumerator.cs \
	Parser/Function.cs \
	Parser/LanguageItem.cs \
	Parser/Macro.cs \
	Parser/Member.cs \
	Parser/Namespace.cs \
	Parser/ProjectInformation.cs \
	Parser/ProjectInformationManager.cs \
	Parser/Structure.cs \
	Parser/Tag.cs \
	Parser/TagDatabaseManager.cs \
	Parser/Typedef.cs \
	Parser/Union.cs \
	Parser/Variable.cs \
	Project/CCompilationParameters.cs \
	Project/CProject.cs \
	Project/CProjectBinding.cs \
	Project/CProjectConfiguration.cs \
	Project/CProjectServiceExtension.cs \
	Project/ProjectPackage.cs \
	Project/ProjectPackageCollection.cs \
	Project/ProjectPackageEventArgs.cs \
	ProjectPad/ProjectNodeExtension.cs \
	ProjectPad/ProjectPackageNodeBuilder.cs \
	ProjectPad/ProjectPackagesFolderNodeBuilder.cs \
	ProjectPad/ProjectReferencesExtension.cs \
	ProjectPad/ProjectResourcesExtension.cs

RES =  \
	CBinding.addin.xml \
	gtk-gui/gui.stetic \
	icons/C.Project.ConsoleProject \
	icons/C.Project.Empty \
	icons/C.Project.SharedLibrary \
	icons/C.Project.StaticLibrary \
	icons/Cpp.Project.Empty \
	icons/Icons.16x16.PrivateUnion \
	icons/Icons.16x16.ProjectReference \
	icons/Icons.16x16.ProtectedUnion \
	icons/Icons.16x16.Union \
	icons/text-x-c++hdr.16x16.png \
	icons/text-x-c++hdr.22x22.png \
	icons/text-x-c++hdr.32x32.png \
	icons/text-x-c++hdr.48x48.png \
	icons/text-x-c++src.16x16.png \
	icons/text-x-c++src.22x22.png \
	icons/text-x-c++src.32x32.png \
	icons/text-x-c++src.48x48.png \
	icons/text-x-chdr.16x16.png \
	icons/text-x-chdr.22x22.png \
	icons/text-x-chdr.32x32.png \
	icons/text-x-chdr.48x48.png \
	icons/text-x-csrc.16x16.png \
	icons/text-x-csrc.22x22.png \
	icons/text-x-csrc.32x32.png \
	icons/text-x-csrc.48x48.png \
	icons/update \
	templates/ConsoleCppProject.xpt.xml \
	templates/ConsoleCProject.xpt.xml \
	templates/EmptyCHeaderFile.xft.xml \
	templates/EmptyCppHeaderFile.xft.xml \
	templates/EmptyCppProject.xpt.xml \
	templates/EmptyCppSourceFile.xft.xml \
	templates/EmptyCProject.xpt.xml \
	templates/EmptyCSourceFile.xft.xml \
	templates/SharedLibraryCppProject.xpt.xml \
	templates/SharedLibraryCProject.xpt.xml \
	templates/StaticLibraryCppProject.xpt.xml \
	templates/StaticLibraryCProject.xpt.xml

all: $(ASSEMBLY)

$(ASSEMBLY): $(build_sources) $(build_resources)
	mkdir -p $(ADDIN_BUILD)
	$(CSC) $(CSC_FLAGS) -debug -out:$@ -target:library $(REFS) \
		$(build_resources:%=/resource:%) $(build_sources)

cbindinglibdir = $(MD_ADDIN_DIR)/BackendBindings
cbindinglib_DATA = $(ASSEMBLY)

CLEANFILES = $(ASSEMBLY) $(ASSEMBLY).mdb

EXTRA_DIST = $(FILES) $(RES)

include $(top_srcdir)/Makefile.include