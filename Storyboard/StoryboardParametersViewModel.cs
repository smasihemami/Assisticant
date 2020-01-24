using System.Collections.Generic;
using System.Linq;

namespace Storyboard
{
	internal class StoryboardParametersViewModel
	{
		private StoryboardParametersModel _model;

		public StoryboardParametersViewModel(StoryboardParametersModel model)
		{
			_model = model;
		}

		public SampleIDDefinitionMode SampleIDDefinitionMode
		{
			get => _model.SampleIDDefinitionModel.DefinitionMode;
			set
			{
				_model.SampleIDDefinitionModel.DefinitionMode = value;
				PopulateSelectedSampleIDsFromAllSampleIDs();
			}
		}

		public int SampleIDNumCharacters
		{
			get => _model.SampleIDDefinitionModel.SampleIDNumCharacters;
			set
			{
				_model.SampleIDDefinitionModel.SampleIDNumCharacters = value;
				PopulateSelectedSampleIDsFromAllSampleIDs();
			}
		}

		private void PopulateSelectedSampleIDsFromAllSampleIDs()
		{
			// when the list of all sample IDs changes, change the _selectedSampleIDs, so that by default all the items
			// in the CheckedComboBox get selected in UI. This is a user requirement specification for our case.
			SelectedSampleIDs.Clear();
			foreach (var sampleID in AllSampleIDs)
			{
				SelectedSampleIDs.Add(sampleID);
			}
		}

		public IEnumerable<SampleID> AllSampleIDs => _model.AllSampleIDs;
		public IList<SampleID> SelectedSampleIDs => _model.SelectedSampleIDs;

		public int MinFilenameLength => _model.SelectedSampleIDs
			.Any()
			? _model.SelectedSampleIDs.SelectMany(x => x.ImageFilesInfo.Select(y => y.Name)).Min(x => x.Length)
			: 0;

		public int UniqueSampleIDCount => _model.SelectedSampleIDs.Count();

		public int ImagesCount => _model.SelectedSampleIDs.Sum(x => x.ImageFilesCount);

		public bool SelectAll => _model.AllSampleIDs.Any();
	}
}